using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovePawnSignal {
    public Vector3 toPosition;
    public int newPosition;
    public int rollCount;
    public Square square;
    public Pawn pawn;

    public MovePawnSignal(Vector3 toPosition, int newPosition, int rollCount, Square square, Pawn pawn) {
        this.toPosition = toPosition;
        this.newPosition = newPosition;
        this.rollCount = rollCount;
        this.pawn = pawn;
        this.square = square;
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
    public Pawn pawn;
    public int previousTurnRoll;
    public string color;
    public Square square;

    public TurnEndSignal(Pawn pawn, int previousTurnRoll, string color, Square square) {
        this.pawn = pawn;
        this.previousTurnRoll = previousTurnRoll;
        this.color = color;
        this.square = square;
    }
}

public struct KillPawnSignal {
    public Pawn pawn;

    public KillPawnSignal(Pawn pawn) {
        this.pawn = pawn;
    }
}