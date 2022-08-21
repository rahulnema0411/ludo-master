using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OfflineMenuController : MonoBehaviour
{
    public Button TwoPlayerPlay, ThreePlayerPlay, FourPlayerPlay, PlayButton;

    private void Start() {
        SetButtons();
    }

    private void SetButtons() {
        TwoPlayerPlay.onClick.RemoveAllListeners();
        TwoPlayerPlay.onClick.AddListener(SetTwoPlayerGame);

        ThreePlayerPlay.onClick.RemoveAllListeners();
        ThreePlayerPlay.onClick.AddListener(SetThreePlayerGame);

        FourPlayerPlay.onClick.RemoveAllListeners();
        FourPlayerPlay.onClick.AddListener(SetFourPlayerGame);

        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(LoadScene);
    }

    private void SetTwoPlayerGame() {
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow");
    }

    private void SetThreePlayerGame() {
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow blue");
    }

    private void SetFourPlayerGame() {
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow blue green");
    }

    private void LoadScene() {
        SceneManager.LoadScene("LudoScene");
    }
}
