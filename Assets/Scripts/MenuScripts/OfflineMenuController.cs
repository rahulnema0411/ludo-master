using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class OfflineMenuController : MonoBehaviour
{
    public ButtonComponent TwoPlayerPlay, ThreePlayerPlay, FourPlayerPlay, PlayButton;
    public Transform selectedModeHighlighter;

    private void Start() {
        SetButtons();
    }

    private void SetButtons() {
        TwoPlayerPlay.SetOnClickListener(SetTwoPlayerGame);

        ThreePlayerPlay.SetOnClickListener(SetThreePlayerGame);

        FourPlayerPlay.SetOnClickListener(SetFourPlayerGame);

        PlayButton.interactable = false;
        PlayButton.SetOnClickListener(LoadScene);
    }

    private void SetTwoPlayerGame() {
        setGamePreferences("false", "red yellow");
        highlightSelectedMode(TwoPlayerPlay.transform);
    }

    private void SetThreePlayerGame() {
        setGamePreferences("false", "red yellow blue");
        highlightSelectedMode(ThreePlayerPlay.transform);
    }

    private void SetFourPlayerGame() {
        setGamePreferences("false", "red yellow blue green");
        highlightSelectedMode(FourPlayerPlay.transform);
    }

    private void LoadScene() {
        SceneManager.LoadScene("LudoScene");
    }
    private void highlightSelectedMode(Transform buttonTransform) {
        selectedModeHighlighter.gameObject.SetActive(true);
        selectedModeHighlighter.DOMove(buttonTransform.transform.position, 0.2f);
        PlayButton.interactable = true;
    }
    private static void setGamePreferences(string isMultiplayer, string turnOrder) {
        PlayerPrefs.SetString("multiplayer", "false");
        PlayerPrefs.SetString("turnOrder", "red yellow");
    }
}
