using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class GameSceneMenu : MonoBehaviour {

    public GameCodePanel gameCodePanel;
    public Transform crossFade;

    public async void WaitAndDoCrossFade() {
        crossFade.gameObject.SetActive(true);
        await UniTask.Delay(500);
        crossFade.DOLocalMoveY(-4000f, 0.5f).OnComplete(delegate() {
            gameObject.SetActive(false);
        });
    }

    public async void WaitAndDoCrossFadeAndDontDisableGameObject() {
        crossFade.gameObject.SetActive(true);
        await UniTask.Delay(500);
        crossFade.DOLocalMoveY(-4000f, 0.5f);
    }
}
