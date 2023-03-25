using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePlayer : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    public void SetAlpha(float v) {
        Color color = image.color;
        color.a = v;
        image.color = color;
    }

    public void SetUser() {
        text.gameObject.SetActive(true);
        text.text = "You";
    }
}
