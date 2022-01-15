using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pawn : MonoBehaviour {


    public string pawnColor;
    public int pawnId;

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
    }

    public void InitPawnValues(string pawnColor, int pawnId) {
        this.pawnColor = pawnColor;
        this.pawnId = pawnId;
    }

    private void Move(MovePawnSignal signal) {
        if (signal.pawnId == pawnId && signal.pawnColor == pawnColor) {
            transform.position = signal.toPosition;
        }
    }

    void Update() {
        DetectHit();
    }

    private IEnumerator MovePawn(float time, Vector3 finalPosition) {
        while (transform.position == new Vector3(2f, 2f, 2f)) {
            yield return new WaitForSeconds(time);
            Vector3.MoveTowards(transform.position, finalPosition, time);
        }
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
}
