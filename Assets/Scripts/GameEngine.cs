using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    public LudoBoard ludoBoard;

    private void Start() {
        ludoBoard.InitializeBoard();
        ludoBoard.Play();
    }
}
