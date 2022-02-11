using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OfflineMenuController : MonoBehaviour
{
    public Button TwoPlayerPlay, ThreePlayerPlay, FourPlayerPlay;

    private void Start() {
        SetButtons();
    }

    private void SetButtons() {
        TwoPlayerPlay.onClick.RemoveAllListeners();
        TwoPlayerPlay.onClick.AddListener(delegate () {
            StartTwoPlayerGame();
        });

        ThreePlayerPlay.onClick.RemoveAllListeners();
        ThreePlayerPlay.onClick.AddListener(delegate () {
            StartThreePlayerGame();
        });

        FourPlayerPlay.onClick.RemoveAllListeners();
        FourPlayerPlay.onClick.AddListener(delegate () {
            StartFourPlayerGame();
        });
    }

    private void StartTwoPlayerGame() {
        string[] TurnOrder = new string[2];
        TurnOrder[0] = "red";
        TurnOrder[1] = "green";
        GameManager.instance.TurnOrder = TurnOrder;
        LoadScene();
    }

    private void StartThreePlayerGame() {
        string[] TurnOrder = new string[3];
        TurnOrder[0] = "red";
        TurnOrder[1] = "green";
        TurnOrder[2] = "yellow";
        GameManager.instance.TurnOrder = TurnOrder;
        LoadScene();
    }

    private void StartFourPlayerGame() {
        string[] TurnOrder = new string[4];
        TurnOrder[0] = "red";
        TurnOrder[1] = "green";
        TurnOrder[2] = "yellow";
        TurnOrder[3] = "blue";
        GameManager.instance.TurnOrder = TurnOrder;
        LoadScene();
    }

    private void LoadScene() {
        SceneManager.LoadScene("LudoScene");
    }
}
