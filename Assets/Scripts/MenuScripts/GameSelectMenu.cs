using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;

public class GameSelectMenu : MonoBehaviour {

    public Slider slider;
    public BaseButton button1P, button2P, button3P, button4P, startGameButton;
    public TextMeshProUGUI playerSelectedText;
    public LudoData ludoData;

    private static readonly float[] sliderValues = { 0.2f, 0.48f, 0.74f, 1f };
    private static readonly string[] playerSelectedValues = { "Single Player\nGame", "Two Players\nGame", "Three Players\nGame", "Four Players\nGame" };
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
            SetLudoData("red");
        });
        button2P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[1], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[1];
            SetLudoData("red green");
        });
        button3P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[2], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[2];
            SetLudoData("red green yellow");
        });
        button4P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[3], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[3];
            SetLudoData("red green yellow blue");
        });

        startGameButton.AddOnClickListener(delegate() {
            _signalBus.Fire(new LoadSceneSignal{ });
        });
    }

    private void SetLudoData(string turnOrder) {
        ludoData.turnOrder = turnOrder;
        ludoData.isMultiplayer = false;
        ludoData.isHost = false;
    }

}
