using UnityEngine;
using Zenject;
using TMPro;

public class GameEngine : MonoBehaviour {

    public LudoBoard ludoBoard;
    public GameSceneMenu gameSceneMenu;
    public GameOverMenu gameOverMenu;
    
    public TextMeshProUGUI userColorText;

    private bool isMultiplayer, host;

    private SendEventMultiplayer _sendEventMultiplayer;
    private ReceiveEventMultiplayer _receiveEventMultiplayer;

    [Inject]
    public void Construct(SendEventMultiplayer sendEventMultiplayer, ReceiveEventMultiplayer receiveEventMultiplayer) {
        _sendEventMultiplayer = sendEventMultiplayer;
        _receiveEventMultiplayer = receiveEventMultiplayer;
    }

    private void Start() {
        GetPlayerPrefsData();
        ludoBoard.InitializeBoard();
        if(isMultiplayer) {
            userColorText.gameObject.SetActive(true);
            if (host) {
                ludoBoard.ActivatePlayers();
                ludoBoard.userColor = ludoBoard.AssignUserColor();
                userColorText.text = ludoBoard.userColor;
                SetDiceResults();
                ludoBoard.Play();
                gameSceneMenu.WaitAndDoCrossFadeAndDontDisableGameObject();
            } else {
                _sendEventMultiplayer.RequestGameDataSignal();
            }
            gameSceneMenu.gameCodePanel.ShowRoomCode();
        } else {
            userColorText.gameObject.SetActive(false);
            ludoBoard.ActivatePlayers();
            ludoBoard.Play();
            gameSceneMenu.WaitAndDoCrossFade();
        }

    }

    public void SetDiceResults() {
        foreach (Player player in ludoBoard.players) {
            if (ludoBoard.userColor.ToLower().Equals(player.diceResult.color.ToLower())) {
                player.diceResult.gameObject.SetActive(false);
            } else {
                player.diceResult.gameObject.SetActive(true);
            }
        }
    }

    private void GetPlayerPrefsData() {
        host = ludoBoard.ludoData.isHost;
        isMultiplayer = ludoBoard.ludoData.isMultiplayer;
    }
}
