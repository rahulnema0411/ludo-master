using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Zenject;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks {

    private SignalBus _signalBus;
    private Menu _menu;

    [Inject]
    public void Construct(SignalBus signalBus, Menu menu) {
        _signalBus = signalBus;
        _menu = menu;
    }

    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected to Server");
        _menu.onlinePlayButton.gameObject.SetActive(true);
    }

    public void JoinLobby() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        Debug.Log("Joined Lobby");
        _signalBus.Fire(new LobbyJoinSignal { });
    }

    public void CreateRoom(string roomName, RoomOptions roomOptions) {
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
        PhotonNetwork.LoadLevel("LudoScene");
    }
}
