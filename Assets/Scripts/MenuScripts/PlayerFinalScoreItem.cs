using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerFinalScoreItem : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI rank, points;
    [SerializeField] private Image playerColor;

    public void SetData(int rankValue, Player player) {
        rank.text = rankValue.ToString();
        points.text = player.Points.ToString();
        playerColor.color = player.playerColor;
    }

}
