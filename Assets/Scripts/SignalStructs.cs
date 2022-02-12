using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovePawnSignal {
    public float toPositionX;
    public float toPositionY;
    public float toPositionZ;
    public int newPosition;
    public int rollCount;
    public int squareId;
    public int pawnID;
    public string pawnColor;
    public bool thrownByFromRESystem;

    public MovePawnSignal(float toPositionX, float toPositionY, float toPositionZ, int newPosition, int rollCount, int squareId, int pawnID, string pawnColor, bool thrownByFromRESystem = false) {
        this.toPositionX = toPositionX;
        this.toPositionY = toPositionY;
        this.toPositionZ = toPositionZ;
        this.newPosition = newPosition;
        this.rollCount = rollCount;
        this.pawnColor = pawnColor;
        this.pawnID = pawnID;
        this.squareId = squareId;
        this.thrownByFromRESystem = thrownByFromRESystem;
    }
}

public struct SelectedPawnSignal {
    public int id;
    public string color;
    public bool thrownByFromRESystem;

    public SelectedPawnSignal(int id, string color, bool thrownByFromRESystem = false) {
        this.id = id;
        this.color = color;
        this.thrownByFromRESystem = thrownByFromRESystem;
    }
}

public struct DiceResultSignal {
    public string color;
    public int roll;
    public bool thrownByFromRESystem;

    public DiceResultSignal(string color, int roll, bool thrownByFromRESystem = false) {
        this.color = color;
        this.roll = roll;
        this.thrownByFromRESystem = thrownByFromRESystem;
    }
}

public struct PlayerTurnSignal {
    public string color;
    public bool thrownByFromRESystem;

    public PlayerTurnSignal(string color, bool thrownByFromRESystem = false) {
        this.color = color;
        this.thrownByFromRESystem = thrownByFromRESystem;
    }
}

public struct TurnEndSignal {
    public int pawnId;
    public string pawnColor;
    public int previousTurnRoll;
    public string color;
    public int squareId;
    public bool thrownByFromRESystem;

    public TurnEndSignal(int pawnId, string pawnColor, int previousTurnRoll, string color, int squareId, bool thrownByFromRESystem = false) {
        this.pawnId = pawnId;
        this.pawnColor = pawnColor;
        this.previousTurnRoll = previousTurnRoll;
        this.color = color;
        this.squareId = squareId;
        this.thrownByFromRESystem = thrownByFromRESystem;
    }
}

public struct KillPawnSignal {
    public string pawnColor;
    public int pawnId;
    public bool thrownByFromRESystem;

    public KillPawnSignal(string pawnColor, int pawnId, bool thrownByFromRESystem = false) {
        this.pawnColor = pawnColor;
        this.pawnId = pawnId;
        this.thrownByFromRESystem = thrownByFromRESystem;
    }
}

public struct LobbyJoinSignal {

}