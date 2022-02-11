using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Menu : MonoBehaviour {

    public OfflineMenuController offlineMenuController;
    public OnlineMenuController onlineMenuController;
    public Transform playModeSelect;
    public Button offlinePlayButton, onlinePlayButton, backButton;

    private ConnectToServer _server;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(ConnectToServer server, SignalBus signalBus) {
        _server = server;
        _signalBus = signalBus;
    }

    private void SubsribeToSignals() {
        _signalBus.Subscribe<LobbyJoinSignal>(OpenLobby);
    }

    private void Start() {
        SubsribeToSignals();
        SetButtons();
        ClearMenu();
        playModeSelect.gameObject.SetActive(true);
    }

    private void SetButtons() {

        offlinePlayButton.onClick.RemoveAllListeners();
        offlinePlayButton.onClick.AddListener(delegate() {
            ClearMenu();
            offlineMenuController.gameObject.SetActive(true);
        });
        
        onlinePlayButton.onClick.RemoveAllListeners();
        onlinePlayButton.onClick.AddListener(delegate() {
            ClearMenu();
            _server.JoinLobby();
        });

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(delegate() {
            ClearMenu();
            playModeSelect.gameObject.SetActive(true);
        });
    }

    private void OpenLobby() {
        onlineMenuController.gameObject.SetActive(true);
    }

    private void ClearMenu() {
        offlineMenuController.gameObject.SetActive(false);
        onlineMenuController.gameObject.SetActive(false);
        playModeSelect.gameObject.SetActive(false);
    }


}
