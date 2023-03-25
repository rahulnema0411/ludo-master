using UnityEngine;
using Zenject;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Newtonsoft.Json;

public class SendEventMultiplayer : MonoBehaviour {

    private SignalBus _signalBus;
    private LudoBoard _ludoBoard;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
    }

    private void Start() {
        SubscribeToSignals();
    }

    private void SubscribeToSignals() {
        _signalBus.Subscribe<PlayerTurnSignal>(SyncPlayerTurnSignal);
        _signalBus.Subscribe<DiceResultSignal>(SyncDiceResultSignal);
        _signalBus.Subscribe<SelectedPawnSignal>(SyncSelectedPawnSignal);
        _signalBus.Subscribe<MovePawnSignal>(SyncMovePawnSignal);
        _signalBus.Subscribe<TurnEndSignal>(SyncTurnEndSignal);
        _signalBus.Subscribe<KillPawnSignal>(SyncKillPawnSignal);
    }

    private void SyncPlayerTurnSignal(PlayerTurnSignal signalData) {
        RaiseEvent(EventCode.PlayerTurnSignalEventCode, new object[] { JsonConvert.SerializeObject(signalData).ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncDiceResultSignal(DiceResultSignal signalData) {
        RaiseEvent(EventCode.DiceResultSignalEventCode, new object[] { JsonConvert.SerializeObject(signalData).ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncSelectedPawnSignal(SelectedPawnSignal signalData) {
        RaiseEvent(EventCode.SelectedPawnSignalEventCode, new object[] { JsonConvert.SerializeObject(signalData).ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncMovePawnSignal(MovePawnSignal signalData) {
        RaiseEvent(EventCode.MovePawnSignalEventCode, new object[] { JsonConvert.SerializeObject(signalData).ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncTurnEndSignal(TurnEndSignal signalData) {
        RaiseEvent(EventCode.TurnEndSignalEventCode, new object[] { JsonConvert.SerializeObject(signalData).ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncKillPawnSignal(KillPawnSignal signalData) {
        RaiseEvent(EventCode.KillPawnSignalEventCode, new object[] { JsonConvert.SerializeObject(signalData).ToString() }, signalData.thrownByFromRESystem);
    }
    
    public void RequestGameDataSignal() {
        RaiseEvent(EventCode.RequestGameDataSignal, new object[] { "" }, false);
    }

    public void FireRoomJoinedSignal() {
        RaiseEvent(EventCode.RoomJoinedSignal, new object[] { _ludoBoard.userColor }, false);
    }
    
    public void SendGameDataSignal() {
        GameData gameData = new GameData();
        gameData.turnOrder = _ludoBoard.ludoData.turnOrder;
        gameData.userColor = _ludoBoard.AssignUserColor();
        gameData.unassignedColors = _ludoBoard.UnassignedColors;
        RaiseEvent(EventCode.GameDataSignal, new object[] { JsonConvert.SerializeObject(gameData).ToString() }, false);
    }

    private static void RaiseEvent(byte EventCode, object[] content, bool thrownByRESystem) {
        if(!thrownByRESystem) {
            PhotonNetwork.RaiseEvent(EventCode, content, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }
}
