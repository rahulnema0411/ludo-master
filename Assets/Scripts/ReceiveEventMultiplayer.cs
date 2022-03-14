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
    private LudoBoard _ludoBoard;
    private SendEventMultiplayer _sendEventMultiplayer;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard, SendEventMultiplayer sendEventMultiplayer) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
        _sendEventMultiplayer = sendEventMultiplayer;
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
                playerTurnSignal.thrownByFromRESystem = true;
                _signalBus.Fire(playerTurnSignal);
                break;
            case EventCode.SelectedPawnSignalEventCode:
                SelectedPawnSignal selectedPawnSignal = JsonConvert.DeserializeObject<SelectedPawnSignal>(s);
                selectedPawnSignal.thrownByFromRESystem = true;
                _signalBus.Fire(selectedPawnSignal);
                break;
            case EventCode.DiceResultSignalEventCode:
                DiceResultSignal diceResultSignal = JsonConvert.DeserializeObject<DiceResultSignal>(s);
                diceResultSignal.thrownByFromRESystem = true;
                _signalBus.Fire(diceResultSignal);
                break;
            case EventCode.MovePawnSignalEventCode:
                MovePawnSignal movePawnSignal = JsonConvert.DeserializeObject<MovePawnSignal>(s);
                movePawnSignal.thrownByFromRESystem = true;
                _signalBus.Fire(movePawnSignal);
                break;
            case EventCode.TurnEndSignalEventCode:
                TurnEndSignal turnEndSignal = JsonConvert.DeserializeObject<TurnEndSignal>(s);
                turnEndSignal.thrownByFromRESystem = true;
                _signalBus.Fire(turnEndSignal);
                break;
            case EventCode.KillPawnSignalEventCode:
                KillPawnSignal killPawnSignal = JsonConvert.DeserializeObject<KillPawnSignal>(s);
                killPawnSignal.thrownByFromRESystem = true;
                _signalBus.Fire(killPawnSignal);
                break;
            case EventCode.RequestTurnOrderSignal:
                if(_ludoBoard.host) {
                    _sendEventMultiplayer.SendTurnOrderSignal();
                }
                break;
            case EventCode.TurnOrderSignal:
                if(!_ludoBoard.host) {
                    PlayerPrefs.SetString("turnOrder", s);
                    _ludoBoard.TurnOrder = s.Split(' ');
                    _ludoBoard.ActivatePlayers();
                    _ludoBoard.Play();
                }
                break;
        }
    }
}
