using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

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
                _server.CreateRoom(createLobbyInput.text);
            } else {
                Debug.LogError("Empty string");
            }
        });
        
        joinLobbyButton.onClick.RemoveAllListeners();
        joinLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(joinLobbyInput.text)) {
                Debug.LogError("Joining room: " + createLobbyInput.text);
                _server.JoinRoom(createLobbyInput.text);
            } else {
                Debug.LogError("Empty string");
            }
        });
    }
}
