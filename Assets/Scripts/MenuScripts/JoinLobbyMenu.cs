using TMPro;
using UnityEngine;
using Zenject;

public class JoinLobbyMenu : MonoBehaviour
{
    public TMP_InputField joinLobbyInput;
    private BaseButton joinLobbyButton;

    public LudoData ludoData;

    private ConnectToServer _server;

    [Inject]
    public void Construct(ConnectToServer server) {
        _server = server;
    }

    void SetButtons() {
        joinLobbyButton.onClick.RemoveAllListeners();
        joinLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(joinLobbyInput.text)) {
                Debug.LogError("Joining room: " + joinLobbyInput.text);
                SetLudoData();
                _server.JoinRoom(joinLobbyInput.text);
            } else {
                Debug.LogError("Empty string");
            }
        });
    }

    private void SetLudoData() {
        ludoData.isMultiplayer = true;
        ludoData.isHost = false;
    }
}
