using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class ColorSelectionPanel : MonoBehaviour {

    public Button Red, Blue, Yellow, Green;
    public Text RedText, BlueText, YellowText, GreenText;
    public Button Submit;
    public Image SelectedColorSprite;
    public TextMeshProUGUI SelectedColorText;

    private LudoBoard _ludoboard;

    [Inject]
    public void Construct(LudoBoard ludoBoard) {
        _ludoboard = ludoBoard;
    }

    private void OnEnable() {
        if(_ludoboard.isMultiplayer) {
            if(_ludoboard.host) {

            } else {

            }
        }
    }

    void Start() {
        
    }
}
