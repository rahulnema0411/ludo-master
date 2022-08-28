using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ButtonComponent : Button {

    private Vector3 initialScale;

    private const float deltaScale = 0.1f;

    private new void Awake() {
        base.Awake();
        initialScale = transform.localScale;
        SetClickAnimation();
    }

    public void AddOnClickListener(Action onClickAction) {
        onClick.AddListener(delegate { onClickAction?.Invoke(); });
    }

    public void SetOnClickListener(Action onClickAction) {
        RemoveAllListeners();
        onClick.AddListener(delegate { onClickAction?.Invoke(); });
    }

    public void RemoveAllListeners() {
        onClick.RemoveAllListeners();
        SetClickAnimation();
    }

    private void SetClickAnimation() {
        onClick.AddListener(DoClickAnimation);
    }

    private void DoClickAnimation() {
        interactable = false;
        transform.DOScale(new Vector3(initialScale.x - deltaScale, initialScale.y - deltaScale, initialScale.z - deltaScale), 0.04f).OnComplete(() => {
            transform.DOScale(initialScale, deltaScale).OnComplete(() => { this.interactable = true; });
        });
    }
}
