using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ColorSelectionPanel : MonoBehaviour {

    public Button Red, Blue, Yellow, Green;
    public Text RedText, BlueText, YellowText, GreenText;
    public Button Submit;
    public Image SelectedColorSprite;
    public TextMeshProUGUI SelectedColorText;
    public Transform crossFade;

    private LudoBoard _ludoboard;

    [Inject]
    public void Construct(LudoBoard ludoBoard) {
        _ludoboard = ludoBoard;
    }

    private void Awake() {
        WaitAndDoCrossFade();
    }

    private async void WaitAndDoCrossFade() {
        crossFade.gameObject.SetActive(true);
        await UniTask.Delay(500);
        crossFade.DOMoveY(-4000f, 0.5f);
    }

    private void OnEnable() {
        if(_ludoboard.isMultiplayer) {
            if(_ludoboard.host) {

            } else {

            }
        }
    }
}
