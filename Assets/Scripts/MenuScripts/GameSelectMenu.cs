using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;

public class GameSelectMenu : MonoBehaviour {

    public Slider slider;
    public BaseButton button1P, button2P, button3P, button4P, startGameButton;
    public TextMeshProUGUI playerSelectedText;

    private static readonly float[] sliderValues = { 0.151f, 0.435f, 0.72f, 1f };
    private static readonly string[] playerSelectedValues = { "Single Player Game", "Two Players Game", "Three Players Game", "Four Players Game" };
    private const float sliderDOMoveDuration = 0.05f;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void Awake() {
        SetButtons();
        button2P.onClick.Invoke();
    }

    private void SetButtons() {
        button1P.AddOnClickListener(delegate() {
            slider.DOValue(sliderValues[0], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[0];
        });
        button2P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[1], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[1];
        });
        button3P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[2], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[2];
        });
        button4P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[3], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[3];
        });

        startGameButton.AddOnClickListener(delegate() {
            _signalBus.Fire(new LoadSceneSignal{ });
        });
    }

}
