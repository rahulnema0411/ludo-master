using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovePawnSignal {
    public Vector3 toPosition;
    public int pawnId;
    public string pawnColor;
    public int newPosition;

    public MovePawnSignal(Vector3 toPosition, int pawnId, string pawnColor, int newPosition) {
        this.toPosition = toPosition;
        this.pawnId = pawnId;
        this.pawnColor = pawnColor;
        this.newPosition = newPosition;
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

public struct DiceResultSignal {
    public string color;
    public int roll;

    public DiceResultSignal(string color, int roll) {
        this.color = color;
        this.roll = roll;
    }
}

public struct PlayerTurnSignal {
    public string color;

    public PlayerTurnSignal(string color) {
        this.color = color;
    }
}

public struct TurnEndSignal {
    public string color;

    public TurnEndSignal(string color) {
        this.color = color;
    }
}