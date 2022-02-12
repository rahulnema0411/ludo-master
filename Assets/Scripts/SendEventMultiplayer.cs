using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Newtonsoft.Json;

public class SendEventMultiplayer : MonoBehaviour {

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
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
        var json = JsonConvert.SerializeObject(signalData);
        RaiseEvent(EventCode.PlayerTurnSignalEventCode, new object[] { json.ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncDiceResultSignal(DiceResultSignal signalData) {
        var json = JsonConvert.SerializeObject(signalData);
        RaiseEvent(EventCode.DiceResultSignalEventCode, new object[] { json.ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncSelectedPawnSignal(SelectedPawnSignal signalData) {
        var json = JsonConvert.SerializeObject(signalData);
        RaiseEvent(EventCode.SelectedPawnSignalEventCode, new object[] { json.ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncMovePawnSignal(MovePawnSignal signalData) {
        var json = JsonConvert.SerializeObject(signalData);
        RaiseEvent(EventCode.MovePawnSignalEventCode, new object[] { json.ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncTurnEndSignal(TurnEndSignal signalData) {
        var json = JsonConvert.SerializeObject(signalData);
        RaiseEvent(EventCode.TurnEndSignalEventCode, new object[] { json.ToString() }, signalData.thrownByFromRESystem);
    }

    private void SyncKillPawnSignal(KillPawnSignal signalData) {
        var json = JsonConvert.SerializeObject(signalData);
        RaiseEvent(EventCode.KillPawnSignalEventCode, new object[] { json.ToString() }, signalData.thrownByFromRESystem);
    }

    private static void RaiseEvent(byte EventCode, object[] content, bool thrownByRESystem) {
        if(!thrownByRESystem) {
            PhotonNetwork.RaiseEvent(EventCode, content, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }
}
