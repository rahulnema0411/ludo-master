using UnityEngine;

public class Fade : MonoBehaviour {

    [SerializeField] private CanvasGroup canvasGroup;
    
    public float fadeInSpeed = 1.0f;
    public float fadeOutSpeed = 1.0f;
    public bool fadeInOnStart = true;

    private float alpha;
    private bool isFadingIn;
    private bool isFadingOut;

    void Start() {

        if (fadeInOnStart) {
            isFadingIn = true;
            alpha = 0.0f;
        }
    }

    void Update() {
        if (isFadingIn) {
            alpha += fadeInSpeed * Time.deltaTime;
            if (alpha >= 1) {
                alpha = 1;
                FadeOut();
            }
        } else if (isFadingOut) {
            alpha -= fadeOutSpeed * Time.deltaTime;
            if (alpha <= 0) {
                alpha = 0;
                FadeIn();
            }
        }
        canvasGroup.alpha = alpha;
    }

    public void FadeIn() {
        isFadingIn = true;
        isFadingOut = false;
    }

    public void FadeOut() {
        isFadingIn = false;
        isFadingOut = true;
    }
}
