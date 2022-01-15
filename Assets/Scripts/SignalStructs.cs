using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovePawnSignal {
    public Vector3 toPosition;
    public int pawnId;
    public string pawnColor;

    public MovePawnSignal(Vector3 toPosition, int pawnId, string pawnColor) {
        this.toPosition = toPosition;
        this.pawnId = pawnId;
        this.pawnColor = pawnColor;
    }
}

public struct SelectedPawnSignal {
    public int id;
    public string color;

    public SelectedPawnSignal(int id, string color) {
        this.id = id;
        this.color = color;
    }
}