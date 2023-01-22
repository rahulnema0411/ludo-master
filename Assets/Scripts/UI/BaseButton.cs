using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class BaseButton : Button {

    [SerializeField]
    private bool clickAnimation = true;

    private Vector3 defaultScale;
    private const float deltaScale = 0.09f;
    private const float deltaDuration = 0.04f;

    private new void Awake() {
        base.Awake();
        defaultScale = transform.localScale;
        //onClick.RemoveAllListeners();
        SetAnimationOnClick();
    }

    /// <summary>
    /// Animation on Click Listener is added
    /// </summary>
    private void SetAnimationOnClick() {
        if (clickAnimation) {
            onClick.AddListener(DoClickAnimation);
        }
    }

    private void DoClickAnimation() {
        this.interactable = false;
        transform.DOScale(new Vector3(defaultScale.x - deltaScale, defaultScale.y - deltaScale, defaultScale.z - deltaScale), deltaDuration).OnComplete(() => {
            transform.DOScale(defaultScale, 0.03f).OnComplete(() => { this.interactable = true; });
        });
    }

    public void AddOnClickListener(Action onClickAction) {
        onClick.AddListener(delegate { onClickAction?.Invoke(); });
    }

    public void SetSingleListener(Action onClickAction) {
        ResetListeners();
        onClick.AddListener(delegate { onClickAction?.Invoke(); });
    }



    /// <summary>
    /// Listeners are removed (Except click animation which depends on 'clickAnimation' state)
    /// </summary>
    public void ResetListeners() {
        onClick.RemoveAllListeners();
        SetAnimationOnClick();
    }
}
