using TMPro;
using UnityEngine;
using Zenject;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField joinLobbyInput;
    [SerializeField] BaseButton joinLobbyButton;

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
        SetButtons();
    }

    void SetButtons() {
        joinLobbyButton.onClick.RemoveAllListeners();
        joinLobbyButton.onClick.AddListener(delegate() { 
            if(!string.IsNullOrWhiteSpace(joinLobbyInput.text)) {
                Debug.Log("Joining room: " + joinLobbyInput.text);
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
