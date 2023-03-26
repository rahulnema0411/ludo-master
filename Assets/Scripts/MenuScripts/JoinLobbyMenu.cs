using TMPro;
using UnityEngine;
using Zenject;

public class JoinLobbyMenu : MonoBehaviour
{
    public TMP_InputField joinLobbyInput;
    private BaseButton joinLobbyButton;

    public LudoData data;

    private ConnectToServer _server;
    private MainMenu _mainMenu;

    [Inject]
    public void Construct(ConnectToServer server, MainMenu mainMenu) {
        _server = server;
        _mainMenu = mainMenu;
    }

    void Start() {
        SetDefaultLudoData();
    }

    void SetButtons() {
        joinLobbyButton.onClick.RemoveAllListeners();
        joinLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(joinLobbyInput.text)) {
                Debug.LogError("Joining room: " + joinLobbyInput.text);
                _mainMenu.SetLudoData(data);
                _server.JoinRoom(joinLobbyInput.text);
            } else {
                Debug.LogError("Empty string");
            }
        });
    }

    private void SetDefaultLudoData() {
        data.isMultiplayer = true;
        data.isHost = false;
        data.turnOrder = "";
    }
}
