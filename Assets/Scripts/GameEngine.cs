using UnityEngine;
using Zenject;
using TMPro;

public class GameEngine : MonoBehaviour {

    public LudoBoard ludoBoard;
    public TextMeshProUGUI userColorText;
    public ColorSelectionPanel colorSelectionPanel;
    public GameOverMenu gameOverMenu;
    

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
                colorSelectionPanel.WaitAndDoCrossFade();
            } else {
                _sendEventMultiplayer.RequestGameDataSignal();
            }
        } else {
            userColorText.gameObject.SetActive(false);
            ludoBoard.ActivatePlayers();
            ludoBoard.Play();
            colorSelectionPanel.WaitAndDoCrossFade();
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
        host = PlayerPrefs.GetString("host", "no") == "yes" ? true : false;
        isMultiplayer = PlayerPrefs.GetString("multiplayer", "false") == "true" ? true : false;
    }
}
