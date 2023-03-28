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
    private GameEngine _gameEngine;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard, SendEventMultiplayer sendEventMultiplayer, GameEngine gameEngine) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
        _sendEventMultiplayer = sendEventMultiplayer;
        _gameEngine = gameEngine;
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
            case EventCode.RequestGameDataSignal:
                if(_ludoBoard.host) {
                    _sendEventMultiplayer.SendGameDataSignal();
                }
                break;
            case EventCode.GameDataSignal:
                if(!_ludoBoard.host) {
                    GameData gameData = JsonConvert.DeserializeObject<GameData>(s);
                    _ludoBoard.ludoData.turnOrder = gameData.turnOrder;
                    _ludoBoard.TurnOrder = gameData.turnOrder.Split(' ');
                    _ludoBoard.ActivatePlayers();
                    _ludoBoard.userColor = gameData.userColor;
                    _ludoBoard.UnassignedColors = gameData.unassignedColors;
                    _gameEngine.userColorText.text = gameData.userColor;
                    _gameEngine.SetDiceResults();
                    _ludoBoard.Play();
                    _sendEventMultiplayer.FireRoomJoinedSignal();
                }
                break;
            case EventCode.RoomJoinedSignal:
                string userJoined = s.ToLower();
                if(!_ludoBoard.host) {
                    _ludoBoard.UpdateUnassignedColorList(userJoined);
                }
                _gameEngine.gameSceneMenu.gameCodePanel.UpdateLobby();
                break;
        }
    }
}
