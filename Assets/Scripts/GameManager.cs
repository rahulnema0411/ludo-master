using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public string[] TurnOrder;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void LoadGameScene() {
        SceneManager.LoadScene("LudoScene");
    }

}
