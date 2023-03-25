using UnityEngine;
using Photon.Pun;
using Zenject;
using Photon.Realtime;
using Cysharp.Threading.Tasks;

public class ConnectToServer : MonoBehaviourPunCallbacks {

    private SignalBus _signalBus;
    private MainMenu _mainMenu;

    [Inject]
    public void Construct(SignalBus signalBus, MainMenu mainMenu) {
        _signalBus = signalBus;
        _mainMenu = mainMenu;
    }

    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected to Server");
    }

    public void JoinLobby() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        Debug.Log("Joined Lobby");
        _signalBus.Fire(new LobbyJoinSignal { });
    }

    public void CreateRoom(string roomName, RoomOptions roomOptions) {
        Debug.Log("Creating room: " + roomName);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void JoinRoom(string roomName) {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.LogError("Join room failed with return Code: " + returnCode);
        Debug.LogError("Join room failed with message: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.LogError("Create room failed with return Code: " + returnCode);
        Debug.LogError("Create room failed with message: " + message);
    }

    public override void OnJoinedRoom() {
        DoCrossFadeAndLoadScene();
    }

    private async void DoCrossFadeAndLoadScene() {
        _mainMenu.DoCrossFade();
        await UniTask.Delay(500);
        PhotonNetwork.LoadLevel("LudoScene");
    }
}
