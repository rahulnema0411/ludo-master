using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LudoBoard : MonoBehaviour {
    [Header("Main Path Way")]
    [SerializeField] public Transform MainPath;
    [SerializeReference] public List<Square> MainPathway;

    public Player[] players;

    //public string[] TurnOrder;

    private int turnIndex;

    private SignalBus _signalBus;
    private GameManager _gameManager;

    [Inject]
    public void Construct(SignalBus signalBus, GameManager gameManager) {
        _signalBus = signalBus;
        _gameManager = gameManager;
    }

    public void InitializeBoard() {
        InitializeMainPath();
        InitializePlayers();
        SubscribeToSignals();
        //ActivatePlayers();
        turnIndex = 0;
    }

    /*private void ActivatePlayers() {
        TurnOrder = new string[4];
        TurnOrder[0] = "red";
        TurnOrder[1] = "green";
        TurnOrder[2] = "yellow";
        TurnOrder[3] = "blue";
    }*/

    private void SubscribeToSignals() {
        _signalBus.Subscribe<TurnEndSignal>(NextTurn);
    }

    private void NextTurn(TurnEndSignal signal) {
        int nextTurn = 0;
        int lastTurn = Array.IndexOf(_gameManager.TurnOrder, signal.color.ToLower());
        if(signal.previousTurnRoll != 6) {
            if(!(lastTurn == _gameManager.TurnOrder.Length - 1)) {
                nextTurn = lastTurn + 1;
            }
        } else {
            nextTurn = lastTurn;
        }
        _signalBus.Fire(new PlayerTurnSignal {
            color = _gameManager.TurnOrder[nextTurn]
        });
    }

    public void Play() {
        _signalBus.Fire(new PlayerTurnSignal { 
            color = _gameManager.TurnOrder[turnIndex]
        });
    }

    private void InitializePlayers() {
        foreach (Player player in players) {
            player.InitializePlayer();
        }
    }

    private void InitializeMainPath() {
        for (int i = 0; i < MainPath.childCount; i++) {
            Square square = MainPath.GetChild(i).GetComponent<Square>();
            square.SetID(i);
            MainPathway.Add(square);
        }
    }
}