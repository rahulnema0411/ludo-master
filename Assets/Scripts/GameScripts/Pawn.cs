using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pawn : MonoBehaviour {

    public CircleCollider2D boxCollider;
    public string pawnColor;
    public int pawnId;
    public int currentPosition;
    public GameObject turnHighlighter;
    public Player Home;
    public bool canMove;
    
    private Vector3 pawnStartingPosition;
    private Vector3 targetPosition;

    private SignalBus _signalBus;

	[Inject]
	public void Construct(SignalBus signalBus) {
		_signalBus = signalBus;
	}

    private void Awake() {
        SubcribeToSignals();
        pawnStartingPosition = transform.position;
        targetPosition = transform.position;
    }

    private void SubcribeToSignals() {
        _signalBus.Subscribe<MovePawnSignal>(Move);
        _signalBus.Subscribe<PlayerTurnSignal>(DisableHit);
        _signalBus.Subscribe<DiceResultSignal>(EnableHitForRespectivePawn);
        _signalBus.Subscribe<KillPawnSignal>(MoveHome);
    }

    public void InitPawnValues(string pawnColor, int pawnId) {
        this.pawnColor = pawnColor;
        this.pawnId = pawnId;
        this.currentPosition = -1;
    }

    private void Move(MovePawnSignal signal) {
        if (signal.pawnID == pawnId && signal.pawnColor == pawnColor && !signal.thrownByFromRESystem) {
            StartCoroutine(HopPawn(signal));
        }
        turnHighlighter.SetActive(false);
    }

    private IEnumerator HopPawn(MovePawnSignal signal) {
        int rollCount = currentPosition == -1 ? 1 : signal.rollCount;
        for (int i = 0; i < rollCount; i++) {
            int newPosition = currentPosition + 1;
            targetPosition = Home.path[newPosition].position;
            yield return new WaitForSeconds(0.2f);
            currentPosition = newPosition;
        }

        FireTurnEndSignal(signal);
        AddPawnReachedHomePoints();
    }

    private void AddPawnReachedHomePoints() {
        if (currentPosition == 56) {
            _signalBus.Fire(new UpdatePlayerPoints {
                color = pawnColor,
                points = 30
            });
        }
    }

    private void FireTurnEndSignal(MovePawnSignal signal) {
        _signalBus.Fire(new TurnEndSignal {
            pawnId = this.pawnId,
            pawnColor = this.pawnColor,
            previousTurnRoll = signal.rollCount,
            color = pawnColor,
            squareId = signal.squareId
        });
    }

    private void MoveHome(KillPawnSignal signalData) {
        if(signalData.pawnColor == pawnColor && signalData.pawnId == pawnId) {
            targetPosition = pawnStartingPosition;
            currentPosition = -1;
        }
    }

    void Update() {
        DetectHit();
        MovePawn();
    }

    private void MovePawn() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);
    }

    private void DetectHit() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.transform.GetComponent<Pawn>() != null) {
                if(hit.transform.GetComponent<Pawn>().pawnId == this.pawnId && hit.transform.GetComponent<Pawn>().pawnColor == this.pawnColor) {
                    _signalBus.Fire(new SelectedPawnSignal(hit.transform.GetComponent<Pawn>().pawnId, hit.transform.GetComponent<Pawn>().pawnColor));
                }
            }
        }        
    }

    private void DisableHit() {
        turnHighlighter.SetActive(false);
        boxCollider.enabled = false;
    }

    private void EnableHitForRespectivePawn(DiceResultSignal signal) {
        if (signal.color.ToLower().Equals(pawnColor.ToLower())) {
            if(signal.roll == 6 && currentPosition == -1) {
                turnHighlighter.SetActive(true);
                boxCollider.enabled = true;
                canMove = true;
            } else {
                if(currentPosition != -1 && currentPosition != 56 && ((currentPosition + signal.roll) < 57)) {
                    turnHighlighter.SetActive(true);
                    boxCollider.enabled = true;
                    canMove = true;
                } else {
                    turnHighlighter.SetActive(false);
                    boxCollider.enabled = false;
                    canMove = false;
                }
            }
        } else {
            boxCollider.enabled = false;
        }
    }
}
