using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class MainMenu : MonoBehaviour {

    public BaseButton tapToPlayButton;
    public GameSelectMenu gameMenu;
    public Transform crossFade;

    private GamePlayData gamePlayData;


    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void SubscribeToSignals() {
        _signalBus.Subscribe<GamePlayData>(SetGamePlayData);
        _signalBus.Subscribe<LoadSceneSignal>(LoadScene);
    }

    private void SetGamePlayData(GamePlayData data) {
        gamePlayData = data;
    }

    private void Awake() {
        SubscribeToSignals();
        SetButtonDelegate();
    }

    private void SetButtonDelegate() {
        tapToPlayButton.SetSingleListener(TapToPlayDelegate);
    }

    private void TapToPlayDelegate() {
        gameMenu.gameObject.SetActive(true);
        tapToPlayButton.gameObject.SetActive(false);
    }

    private async void LoadScene() {
        crossFade.DOMoveY(1024f, 0.5f);
        await UniTask.Delay(500);
        SceneManager.LoadScene("LudoScene");
    }

    private void UnSubscribeToSignals() {
        _signalBus.TryUnsubscribe<GamePlayData>(SetGamePlayData);
        _signalBus.TryUnsubscribe<LoadSceneSignal>(LoadScene);
    }

    private void OnDestroy() {
        UnSubscribeToSignals();
    }
}
