using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;

public class GameSelectMenu : MonoBehaviour {

    public Slider slider;
    public BaseButton button1P, button2P, button3P, button4P, startGameButton;
    public TextMeshProUGUI playerSelectedText;

    private static readonly float[] sliderValues = { 0.2f, 0.48f, 0.74f, 1f };
    private static readonly string[] playerSelectedValues = { "Single Player\nGame", "Two Players\nGame", "Three Players\nGame", "Four Players\nGame" };
    private const float sliderDOMoveDuration = 0.05f;
    public LudoData data;

    private SignalBus _signalBus;
    private MainMenu _mainMenu;

    [Inject]
    public void Construct(SignalBus signalBus, MainMenu mainMenu) {
        _signalBus = signalBus;
        _mainMenu = mainMenu;
    }

    private void Awake() {
        SetButtons();
    }

    private void Start() {
        button2P.onClick.Invoke();
        SetLocalLudoData("red green");
    }

    private void SetButtons() {
        button1P.AddOnClickListener(delegate() {
            slider.DOValue(sliderValues[0], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[0];
            SetLocalLudoData("red");
        });
        button2P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[1], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[1];
            SetLocalLudoData("red green");
        });
        button3P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[2], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[2];
            SetLocalLudoData("red green yellow");
        });
        button4P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[3], sliderDOMoveDuration);
            playerSelectedText.text = playerSelectedValues[3];
            SetLocalLudoData("red green yellow blue");
        });

        startGameButton.AddOnClickListener(delegate() {
            _mainMenu.SetLudoData(data);
            _signalBus.Fire(new LoadSceneSignal{ });
        });
    }

    private void SetLocalLudoData(string turnOrder) {
        data.isHost = false;
        data.isMultiplayer = false;
        data.turnOrder = turnOrder;
    }

}
