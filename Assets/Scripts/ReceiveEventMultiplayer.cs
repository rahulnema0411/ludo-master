using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Newtonsoft.Json;

public class ReceiveEventMultiplayer : MonoBehaviour, IOnEventCallback {

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent) {

        byte eventCode = photonEvent.Code;

        if(eventCode == 255) {
            return;
        }

        object[] data = (object[])photonEvent.CustomData;
        string s = string.Empty;
        if (data != null && data.Length > 0) {
            s = (string)data[0];
        }

        switch (eventCode) {
            case EventCode.PlayerTurnSignalEventCode:
                PlayerTurnSignal playerTurnSignal = JsonConvert.DeserializeObject<PlayerTurnSignal>(s);
                _signalBus.Fire(playerTurnSignal);
                break;
            case EventCode.SelectedPawnSignalEventCode:
                SelectedPawnSignal selectedPawnSignal = JsonConvert.DeserializeObject<SelectedPawnSignal>(s);
                _signalBus.Fire(selectedPawnSignal);
                break;
            case EventCode.DiceResultSignalEventCode:
                DiceResultSignal diceResultSignal = JsonConvert.DeserializeObject<DiceResultSignal>(s);
                _signalBus.Fire(diceResultSignal);
                break;
            case EventCode.MovePawnSignalEventCode:
                MovePawnSignal movePawnSignal = JsonConvert.DeserializeObject<MovePawnSignal>(s);
                _signalBus.Fire(movePawnSignal);
                break;
            case EventCode.TurnEndSignalEventCode:
                TurnEndSignal turnEndSignal = JsonConvert.DeserializeObject<TurnEndSignal>(s);
                _signalBus.Fire(turnEndSignal);
                break;
            case EventCode.KillPawnSignalEventCode:
                KillPawnSignal killPawnSignal = JsonConvert.DeserializeObject<KillPawnSignal>(s);
                _signalBus.Fire(killPawnSignal);
                break;
        }
    }
}