using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Square : MonoBehaviour
{
    public Vector3 position;
    public string label = "";
    public int id = 0;
    public List<Pawn> pawnsOnThisSquare;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void Awake() {
        SubcribeToSignals();
    }

    private void SubcribeToSignals() {
        _signalBus.Subscribe<MovePawnSignal>(UpdatePawnsOnThisSquare);
        _signalBus.Subscribe<TurnEndSignal>(CheckForPawnKills);
    }

    private void UpdatePawnsOnThisSquare(MovePawnSignal signalData) {
        if(signalData.square.id == id) {
            pawnsOnThisSquare.Add(signalData.pawn);
        } else {
            if(pawnsOnThisSquare.Count > 0) {
                Pawn pawn = pawnsOnThisSquare.Find(x => x.pawnId == signalData.pawn.pawnId && x.pawnColor == signalData.pawn.pawnColor);
                if(pawn != null) {
                    pawnsOnThisSquare.Remove(pawn);
                }
            }
        }
    }

    private void CheckForPawnKills(TurnEndSignal signalData) {
        StartCoroutine(CheckAndKillPawn(signalData));
    }

    // Start is called before the first frame update
    void Start() {
        position = transform.position;
        this.pawnsOnThisSquare = new List<Pawn>();
    }

    public void SetID(int id) {
        this.id = id;
    }

    private IEnumerator CheckAndKillPawn(TurnEndSignal signalData) {
        yield return new WaitForSeconds(0.25f);
        if(signalData.square != null && signalData.pawn != null) {
            if (signalData.square.id == id && pawnsOnThisSquare.Count > 1 && !label.ToLower().Contains("star")) {
                foreach(Pawn pawn in pawnsOnThisSquare) {
                    if(pawn.pawnColor != signalData.pawn.pawnColor) {
                        _signalBus.Fire(new KillPawnSignal { 
                            pawn = pawn
                        });
                    }
                }
            }
        }
    }
}
