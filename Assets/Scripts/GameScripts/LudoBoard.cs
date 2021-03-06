using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LudoBoard : MonoBehaviour {
    [Header("Main Path Way")]
    [SerializeField] public Transform MainPath;
    [SerializeReference] public List<Square> MainPathway;

    public Player[] players;

    public string[] TurnOrder;
    public List<string> UnassignedColors;
    public bool isMultiplayer;
    public bool host;
    public string userColor;

    private int turnIndex;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void InitializeBoard() {
        GetPlayerPrefsData();
        InitializeMainPath();
        InitializePlayers();
        SubscribeToSignals();
        turnIndex = 0;
    }

    public void ActivatePlayers() {
        string turnOrder = PlayerPrefs.GetString("turnOrder", "red yellow");
        TurnOrder = turnOrder.Split(' ');
        UnassignedColors = TurnOrder.ToList();
        foreach(Player player in players) {
            if(turnOrder.Contains(player.color.ToLower())) {
                player.gameObject.SetActive(true);
            } else {
                player.gameObject.SetActive(false);
            }
        }
    }

    public string AssignUserColor() {
        string assignedColor;
        if( UnassignedColors.Count > 0 ) {
            System.Random random = new System.Random();
            int randomVal = random.Next(0, UnassignedColors.Count);
            assignedColor = UnassignedColors[randomVal];
            UnassignedColors.RemoveAt(UnassignedColors.IndexOf(assignedColor));
        } else {
            assignedColor = "";
        }
        return assignedColor;
    }

    private void GetPlayerPrefsData() {
        host = PlayerPrefs.GetString("host", "no") == "yes" ? true : false;
        isMultiplayer = PlayerPrefs.GetString("multiplayer", "false") == "true" ? true : false;
    }

    private void SubscribeToSignals() {
        _signalBus.Subscribe<TurnEndSignal>(NextTurn);
    }

    private void NextTurn(TurnEndSignal signal) {
        int nextTurn = 0;
        int lastTurn = Array.IndexOf(TurnOrder, signal.color.ToLower());
        if(signal.previousTurnRoll != 6) {
            if(!(lastTurn == TurnOrder.Length - 1)) {
                nextTurn = lastTurn + 1;
            }
        } else {
            nextTurn = lastTurn;
        }
        _signalBus.Fire(new PlayerTurnSignal {
            color = TurnOrder[nextTurn]
        });
    }

    public void Play() {
        _signalBus.Fire(new PlayerTurnSignal { 
            color = TurnOrder[turnIndex]
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
