using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour {
    [SerializeField] public List<Square> path;
    [SerializeField] public Transform FinalPath;
    [SerializeField] public Pawn[] pawns;
    [SerializeField] public Dice dice;
    [SerializeField] public string color;
    [SerializeField] public string start;
    [SerializeField] public string end;

    private int roll;

    private LudoBoard _ludoBoard;
    private SignalBus _signalBus;


    [Inject]
    public void Construct(LudoBoard ludoBoard, SignalBus signalBus) {
        _ludoBoard = ludoBoard;
        _signalBus = signalBus;
    }

    public void InitializePlayer() {
        SetPawnValue();
        SetPath();
        SubscribeSignals();
    }

    private void SubscribeSignals() {
        _signalBus.Subscribe<DiceResultSignal>(StoreDiceResult);
        _signalBus.Subscribe<SelectedPawnSignal>(MovePawn);
    }

    private void StoreDiceResult(DiceResultSignal signal) {
        if(signal.color.Equals(color)) {
            if (isEveryPawnHome()) {
                if(signal.roll == 6) {
                    roll = signal.roll;
                } else {
                    _signalBus.Fire(new TurnEndSignal {
                        color = color
                    });
                }
            } else {
                roll = signal.roll;
            }
        }   
    }

    private void MovePawn(SelectedPawnSignal signal) {
        if (signal.color.Equals(color)) {
            int currentPosition = pawns[signal.id].currentPosition;

            int newPosition;
            if (currentPosition == -1) {
                newPosition = 0;
            } else {
                newPosition = currentPosition + roll;
            }
            Vector3 newVectorPosition = path[newPosition].position;

            _signalBus.Fire(
                new MovePawnSignal(
                    toPosition: newVectorPosition,
                    pawnId: signal.id,
                    pawnColor: signal.color,
                    newPosition: newPosition,
                    rollCount: roll
                )
            );
            roll = 0;
        }
    }

    private void SetPawnValue() {
        int index = 0;
        foreach (Pawn pawn in pawns) {
            pawn.InitPawnValues(color, index);
            index++;
        }
    }

    private void SetPath() {
        int startIndex = _ludoBoard.MainPathway.FindIndex(x => x.label == start);
        int endIndex = _ludoBoard.MainPathway.FindIndex(x => x.label == end);
        for (int i = startIndex; i < _ludoBoard.MainPathway.Count; i++) {
            path.Add(_ludoBoard.MainPathway[i]);
        }
        for (int i = 0; i <= endIndex; i++) {
            path.Add(_ludoBoard.MainPathway[i]);
        }
        for(int i = 0;i < FinalPath.childCount; i++) {
            path.Add(FinalPath.GetChild(i).GetComponent<Square>());
        }
    }

    private bool isEveryPawnHome() {
        foreach (Pawn pawn in pawns) {
            if(pawn.currentPosition != -1) {
                return false;
            }
        }
        return true;
    }
}
