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
    
    private Vector3 pawnStartingPosition;

    private SignalBus _signalBus;

	[Inject]
	public void Construct(SignalBus signalBus) {
		_signalBus = signalBus;
	}

    private void Awake() {
        SubcribeToSignals();
        pawnStartingPosition = transform.position;
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
            transform.position = new Vector3(signal.toPositionX, signal.toPositionY, signal.toPositionZ);
            currentPosition = signal.newPosition;
            _signalBus.Fire(new TurnEndSignal { 
                pawnId = this.pawnId,
                pawnColor = this.pawnColor,
                previousTurnRoll = signal.rollCount,
                color = pawnColor, 
                squareId = signal.squareId
            });
        }
        turnHighlighter.SetActive(false);
    }

    private void MoveHome(KillPawnSignal signalData) {
        if(signalData.pawnColor == pawnColor && signalData.pawnId == pawnId) {
            transform.position = pawnStartingPosition;
            currentPosition = -1;
        }
    }

    void Update() {
        DetectHit();
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
        if(signal.color.ToLower().Equals(pawnColor.ToLower())) {
            if(signal.roll == 6) {
                turnHighlighter.SetActive(true);
                boxCollider.enabled = true;
            } else { 
                if(currentPosition != -1) {
                    turnHighlighter.SetActive(true);
                    boxCollider.enabled = true;
                }
            }
        } else {
            boxCollider.enabled = false;
        }
    }
}
