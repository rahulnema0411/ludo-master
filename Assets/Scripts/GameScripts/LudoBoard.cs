using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LudoBoard : MonoBehaviour {
    [Header("Main Path Way")]
    [SerializeField] public Transform MainPath;
    [SerializeField] public Transform RedFinalPath;
    [SerializeField] public Transform BlueFinalPath;
    [SerializeField] public Transform YellowFinalPath;
    [SerializeField] public Transform GreenFinalPath;
    [SerializeReference] public List<Square> MainPathway;

    [Header("Pawns")]
    [SerializeField] public Pawn[] RedPawns;
    [SerializeField] public Pawn[] BluePawns;
    [SerializeField] public Pawn[] GreenPawns;
    [SerializeField] public Pawn[] YellowPawns;

    [SerializeField] public List<Square> _redPath;
    [SerializeField] public List<Square> _bluePath;
    [SerializeField] public List<Square> _yellowPath;
    [SerializeField] public List<Square> _greenPath;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void Awake() {
        SubcribeToSignals();
    }

    private void SubcribeToSignals() {
        _signalBus.Subscribe<SelectedPawnSignal>(MovePawn);
    }

    public void InitializeBoard() {
        InitializeMainPath();
        InitializeAllPlayerPaths();
        InitializePawns();
    }
    private void InitializeMainPath() {
        for (int i = 0; i < MainPath.childCount; i++) {
            MainPathway.Add(MainPath.GetChild(i).GetComponent<Square>());
        }
    }

    private void InitializeAllPlayerPaths() {
        SetPath("RedStart", "RedEnd", _redPath);
        SetPath("BlueStart", "BlueEnd", _bluePath);
        SetPath("YellowStart", "YellowEnd", _yellowPath);
        SetPath("GreenStart", "GreenEnd", _greenPath);
    }

    private void InitializePawns() {
        SetPawnValue(RedPawns, "red");
        SetPawnValue(BluePawns, "blue");
        SetPawnValue(GreenPawns, "green");
        SetPawnValue(YellowPawns, "yellow");
    }

    private void SetPawnValue(Pawn[] pawns, string color) {
        int index = 0;
        foreach(Pawn pawn in pawns) {
            pawn.InitPawnValues(color, index);
            index++;
        }
    }

    private void SetPath(string start, string end, List<Square> path) {
        int startIndex = MainPathway.FindIndex(x => x.label == start);
        int endIndex = MainPathway.FindIndex(x => x.label == end);
        for (int i = startIndex;i < MainPathway.Count;i++) {
            path.Add(MainPathway[i]);
        }
        for (int i = 0; i <= endIndex; i++) {
            path.Add(MainPathway[i]);
        }
    }

    private void MovePawn(SelectedPawnSignal signal) {
        switch(signal.color) {
            case "red":
                _signalBus.Fire(
                    new MovePawnSignal(
                        toPosition: _redPath[0]._position, 
                        pawnId: signal.id, 
                        pawnColor: signal.color
                    ));
                break;
            case "blue":
                _signalBus.Fire(
                    new MovePawnSignal(
                        toPosition: _bluePath[0]._position,
                        pawnId: signal.id,
                        pawnColor: signal.color
                    ));
                break;
            case "yellow":
                _signalBus.Fire(
                    new MovePawnSignal(
                        toPosition: _yellowPath[0]._position,
                        pawnId: signal.id,
                        pawnColor: signal.color
                    ));
                break;
            case "green":
                _signalBus.Fire(
                    new MovePawnSignal(
                        toPosition: _greenPath[0]._position,
                        pawnId: signal.id,
                        pawnColor: signal.color
                    ));
                break;
        }
        
    }
}
