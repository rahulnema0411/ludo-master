using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using Photon.Realtime;
using System;

public class OnlineMenuController : MonoBehaviour {

    public TMP_InputField createLobbyInput, joinLobbyInput;
    public Button createLobbyButton, joinLobbyButton;
    public Button twoPlayer, threePlayer, fourPlayer;
    public TextMeshProUGUI gameSelectedText;

    private ConnectToServer _server;

    [Inject]
    public void Construct(ConnectToServer server) {
        _server = server;
    }

    private void Start() {
        SetButtons();
        SetText();
    }

    private void SetText() {
        gameSelectedText.text = "2P";
    }

    private void SetButtons() {

        createLobbyButton.onClick.RemoveAllListeners();
        createLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(createLobbyInput.text)) {
                Debug.LogError("Creating room: " + createLobbyInput.text);
                GameManager.instance.isMultiplayer = true;
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 4;
                _server.CreateRoom(createLobbyInput.text, roomOptions);
            } else {
                Debug.LogError("Empty string");
            }
        });
        
        joinLobbyButton.onClick.RemoveAllListeners();
        joinLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(joinLobbyInput.text)) {
                Debug.LogError("Joining room: " + joinLobbyInput.text);
                GameManager.instance.isMultiplayer = true;
                SetTurnOrder();
                _server.JoinRoom(joinLobbyInput.text);
            } else {
                Debug.LogError("Empty string");
            }
        });

        twoPlayer.onClick.RemoveAllListeners();
        twoPlayer.onClick.AddListener(delegate() {
            gameSelectedText.text = "2P";
            SetTurnOrder(2);
        });


        threePlayer.onClick.RemoveAllListeners();
        threePlayer.onClick.AddListener(delegate() {
            gameSelectedText.text = "3P";
            SetTurnOrder(3);
        });
        
        
        fourPlayer.onClick.RemoveAllListeners();
        fourPlayer.onClick.AddListener(delegate() {
            gameSelectedText.text = "4P";
            SetTurnOrder(4);
        });
    }

    private static void SetTurnOrder(int players = 2) {
        string[] TurnOrder = new string[0];
        switch (players) {
            case 2:
                TurnOrder = new string[2];
                TurnOrder[0] = "red";
                TurnOrder[1] = "yellow";
                break;
            case 3:
                TurnOrder = new string[3];
                TurnOrder[0] = "red";
                TurnOrder[1] = "yellow";
                TurnOrder[2] = "green";
                break;
            case 4:
                TurnOrder = new string[4];
                TurnOrder[0] = "red";
                TurnOrder[1] = "green";
                TurnOrder[2] = "yellow";
                TurnOrder[3] = "blue";
                break;
        }
        GameManager.instance.TurnOrder = TurnOrder;
    }
}
