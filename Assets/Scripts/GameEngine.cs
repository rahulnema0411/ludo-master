using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class GameEngine : MonoBehaviour {

    public LudoBoard ludoBoard;
    public ColorSelectionPanel selectionPanel;
    public TextMeshProUGUI userColorText;

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
            userColorText.gameObject.SetActive(true);
            if (host) {
                ludoBoard.ActivatePlayers();
                userColorText.text = ludoBoard.AssignUserColor();
                ludoBoard.Play();
            } else {
                _sendEventMultiplayer.RequestGameDataSignal();
            }
        } else {
            userColorText.gameObject.SetActive(false);
            ludoBoard.ActivatePlayers();
            ludoBoard.Play();
        }

    }

    private void GetPlayerPrefsData() {
        host = PlayerPrefs.GetString("host", "no") == "yes" ? true : false;
        isMultiplayer = PlayerPrefs.GetString("multiplayer", "false") == "true" ? true : false;
    }
}
