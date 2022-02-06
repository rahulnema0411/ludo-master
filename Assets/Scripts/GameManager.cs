using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[] TurnOrder;

    void Awake() {
        MakeThisTheOnlyGameManager();
    }


    void MakeThisTheOnlyGameManager() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }
    }
}
