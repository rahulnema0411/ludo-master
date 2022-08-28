using UnityEngine;

public class AdjustSize : MonoBehaviour {

    public RectTransform rectTransform;

    private void Awake() {
        rectTransform.sizeDelta = new Vector3(Screen.width, Screen.width);
    }

}
