using UnityEngine;

public class MainMenu : MonoBehaviour {

    public BaseButton tapToPlayButton;
    public GameSelectMenu gameMenu;

    private void Awake() {
        SetButtonDelegate();
    }

    private void SetButtonDelegate() {
        tapToPlayButton.SetSingleListener(TapToPlayDelegate);
    }

    private void TapToPlayDelegate() {
        gameMenu.gameObject.SetActive(true);
        tapToPlayButton.gameObject.SetActive(false);
    }

    
}
