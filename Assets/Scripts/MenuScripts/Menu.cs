using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Menu : MonoBehaviour {

    public Button TwoPlayerPlay, ThreePlayerPlay, FourPlayerPlay;

    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager) {
        _gameManager = gameManager;
    }

    private void Start() {
        SetButtons();
    }

    private void SetButtons() {
        TwoPlayerPlay.onClick.RemoveAllListeners();
        TwoPlayerPlay.onClick.AddListener(delegate() {
            StartTwoPlayerGame();
        });

        ThreePlayerPlay.onClick.RemoveAllListeners();
        ThreePlayerPlay.onClick.AddListener(delegate() {
            StartThreePlayerGame();
        });
        
        FourPlayerPlay.onClick.RemoveAllListeners();
        FourPlayerPlay.onClick.AddListener(delegate() {
            StartFourPlayerGame();
        });
    }

    private void StartTwoPlayerGame() {
        string[] turnOrder = new string[2];
        turnOrder[0] = "red";
        turnOrder[1] = "green";
        _gameManager.TurnOrder = turnOrder;
        _gameManager.LoadGameScene();
    }

    private void StartThreePlayerGame() {
        string[] turnOrder = new string[3];
        turnOrder[0] = "red";
        turnOrder[1] = "green";
        turnOrder[2] = "yellow";
        _gameManager.TurnOrder = turnOrder;
        _gameManager.LoadGameScene();
    }

    private void StartFourPlayerGame() {
        string[] turnOrder = new string[4];
        turnOrder[0] = "red";
        turnOrder[1] = "green";
        turnOrder[2] = "yellow";
        turnOrder[3] = "blue";
        _gameManager.TurnOrder = turnOrder;
        _gameManager.LoadGameScene();
    }
}
