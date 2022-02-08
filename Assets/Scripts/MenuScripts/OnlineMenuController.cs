using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using Photon.Realtime;

public class OnlineMenuController : MonoBehaviour {

    public TMP_InputField createLobbyInput, joinLobbyInput;
    public Button createLobbyButton, joinLobbyButton;

    private ConnectToServer _server;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(ConnectToServer server, SignalBus signalBus) {
        _server = server;
        _signalBus = signalBus;
    }

    private void Start() {
        SetButtons();
    }

    private void SetButtons() {

        createLobbyButton.onClick.RemoveAllListeners();
        createLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(createLobbyInput.text)) {
                Debug.LogError("Creating room: " + createLobbyInput.text);
                SetTurnOrder();
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 2;
                _server.CreateRoom(createLobbyInput.text, roomOptions);
            } else {
                Debug.LogError("Empty string");
            }
        });
        
        joinLobbyButton.onClick.RemoveAllListeners();
        joinLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(joinLobbyInput.text)) {
                Debug.LogError("Joining room: " + joinLobbyInput.text);
                SetTurnOrder();
                _server.JoinRoom(joinLobbyInput.text);
            } else {
                Debug.LogError("Empty string");
            }
        });
    }

    private static void SetTurnOrder() {
        string[] TurnOrder = new string[2];
        TurnOrder[0] = "red";
        TurnOrder[1] = "yellow";
        GameManager.instance.TurnOrder = TurnOrder;
    }
}
