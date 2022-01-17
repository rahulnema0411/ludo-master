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

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void InitializeBoard() {
        InitializeMainPath();
        InitializePlayers();
    }

    private void InitializePlayers() {
        foreach (Player player in players) {
            player.InitializePlayer();
        }
    }

    private void InitializeMainPath() {
        for (int i = 0; i < MainPath.childCount; i++) {
            MainPathway.Add(MainPath.GetChild(i).GetComponent<Square>());
        }
    }
}
