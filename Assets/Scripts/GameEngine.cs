using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEngine : MonoBehaviour {

    public LudoBoard ludoBoard;
    public ColorSelectionPanel selectionPanel;

    private bool isMultiplayer, host;

    private SendEventMultiplayer _sendEventMultiplayer;
    private ReceiveEventMultiplayer _receiveEventMultiplayer;

    [Inject]
    public void Construct(SendEventMultiplayer sendEventMultiplayer, ReceiveEventMultiplayer receiveEventMultiplayer) {
        _sendEventMultiplayer = sendEventMultiplayer;
        _receiveEventMultiplayer = receiveEventMultiplayer;
    }

    private void Start() {
        GetPlayerPrefsData();
        ludoBoard.InitializeBoard();
        if(isMultiplayer) {
            //selectionPanel.gameObject.SetActive(true);
            if (host) {
                ludoBoard.ActivatePlayers();
                ludoBoard.Play();
            } else {
                _sendEventMultiplayer.RequestTurnOrderSignal();
            }
        } else {
            ludoBoard.ActivatePlayers();
            ludoBoard.Play();
        }

    }

    private void GetPlayerPrefsData() {
        host = PlayerPrefs.GetString("host", "no") == "yes" ? true : false;
        isMultiplayer = PlayerPrefs.GetString("multiplayer", "false") == "true" ? true : false;
    }
}
