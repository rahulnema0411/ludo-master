using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class GameCodePanel : MonoBehaviour
{
    [SerializeField] GameObject roomCodePanel;
    [SerializeField] TextMeshProUGUI roomCodeText;
    [SerializeField] BaseButton copyRoomCodeButton;
    [SerializeField] List<OnlinePlayer> players;

    private LudoBoard _ludoboard;

    [Inject]
    public void Construct(LudoBoard ludoBoard) {
        _ludoboard = ludoBoard;
    }

    private string[] TurnOrder { get; set; }

    public void ShowRoomCode() {
        
        SetUserColor();

        roomCodePanel.SetActive(_ludoboard.ludoData.isHost);

        roomCodeText.text = _ludoboard.ludoData.lobbyCode;
        
        copyRoomCodeButton.onClick.RemoveAllListeners();
        copyRoomCodeButton.onClick.AddListener(delegate () {
            GUIUtility.systemCopyBuffer = _ludoboard.ludoData.lobbyCode;
        });

        UpdateLobby();
    }

    public void UpdateLobby() {
        if(_ludoboard.UnassignedColors != null && _ludoboard.UnassignedColors.Count > 0) {

            TurnOrder = _ludoboard.ludoData.turnOrder.Split(' ');

            foreach(string player in TurnOrder) {
                OnlinePlayer playerImage = players.Find(x => (x.transform.name.ToLower() == player));
                playerImage.SetAlpha(1.0f);
                playerImage.gameObject.SetActive(true);
            }

            foreach(string player in _ludoboard.UnassignedColors) {
                OnlinePlayer playerImage = players.Find(x => (x.transform.name.ToLower() == player));
                playerImage.SetAlpha(0.5f);
            }
            

            gameObject.SetActive(true);

        } else {
            gameObject.SetActive(false);
        }    
    }

    private void SetUserColor() {
        OnlinePlayer playerImage = players.Find(x => (x.transform.name.ToLower() == _ludoboard.userColor));
        if(playerImage != null) {
            playerImage.SetUser();
        }  else {
            Debug.LogError("Color not found:" + _ludoboard.userColor);
        }
    }
}
