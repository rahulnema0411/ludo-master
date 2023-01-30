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
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow");
        LoadScene();
    }

    private void StartThreePlayerGame() {
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow blue");
        LoadScene();
    }

    private void StartFourPlayerGame() {
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow blue green");
        LoadScene();
    }

    private void LoadScene() {
        SceneManager.LoadScene("LudoScene");
    }
}
