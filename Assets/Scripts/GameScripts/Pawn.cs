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

    private SignalBus _signalBus;

	[Inject]
	public void Construct(SignalBus signalBus) {
		_signalBus = signalBus;
	}

    private void Awake() {
        SubcribeToSignals();
    }

    private void SubcribeToSignals() {
        _signalBus.Subscribe<MovePawnSignal>(Move);
        _signalBus.Subscribe<PlayerTurnSignal>(DisableHit);
        _signalBus.Subscribe<DiceResultSignal>(EnableHitForRespectivePawn);
    }

    public void InitPawnValues(string pawnColor, int pawnId) {
        this.pawnColor = pawnColor;
        this.pawnId = pawnId;
        this.currentPosition = -1;
    }

    private void Move(MovePawnSignal signal) {
        if (signal.pawnId == pawnId && signal.pawnColor == pawnColor) {
            transform.position = signal.toPosition;
            currentPosition = signal.newPosition;
            _signalBus.Fire(new TurnEndSignal { 
                color = pawnColor
            });
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
        boxCollider.enabled = false;
    }

    private void EnableHitForRespectivePawn(DiceResultSignal signal) {
        if(signal.color.ToLower().Equals(pawnColor.ToLower())) {
            boxCollider.enabled = true;
        } else {
            boxCollider.enabled = false;
        }
    }
}
