using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour {
    [SerializeField] public List<Square> path;
    [SerializeField] public Transform FinalPath;
    [SerializeField] public Pawn[] pawns;
    [SerializeField] public Dice dice;
    [SerializeField] public DiceResult diceResult;
    [SerializeField] public string color;
    [SerializeField] public string start;
    [SerializeField] public string end;
    [SerializeField] public Animator backGroundAnimator;
    [SerializeField] public Vector2 startPos, nextPos, endPos;

    private int roll;

    private LudoBoard _ludoBoard;
    private SignalBus _signalBus;


    [Inject]
    public void Construct(LudoBoard ludoBoard, SignalBus signalBus) {
        _ludoBoard = ludoBoard;
        _signalBus = signalBus;
    }

    public void InitializePlayer() {
        SetPawnValue();
        //SetPath();
        InitializePath(start:((int)startPos.x, (int)startPos.y), next: ((int)nextPos.x, (int)nextPos.y), end:((int)endPos.x, (int)endPos.y));
        SubscribeSignals();
    }

    private void SetDiceResult() {
        if(_ludoBoard.isMultiplayer) {
            diceResult.gameObject.SetActive(true);
        } else {
            diceResult.gameObject.SetActive(false);
        }
    }

    private void SubscribeSignals() {
        _signalBus.Subscribe<DiceResultSignal>(StoreDiceResult);
        _signalBus.Subscribe<SelectedPawnSignal>(MovePawn);
        _signalBus.Subscribe<PlayerTurnSignal>(AnimateHome);
        _signalBus.Subscribe<TurnEndSignal>(StopHomeAnimation);
    }

    private void AnimateHome(PlayerTurnSignal signal) {
        if(signal.color.Equals(color.ToLower())) {
            backGroundAnimator.SetBool("Shine", true);
        }
    }
    
    private void StopHomeAnimation(TurnEndSignal signal) {
        backGroundAnimator.SetBool("Shine", false);
    }

    private void StoreDiceResult(DiceResultSignal signal) {
        if(signal.color.Equals(color)) {
            if (isEveryPawnHome()) {
                if (signal.roll == 6) {
                    roll = signal.roll;
                } else {
                    _signalBus.Fire(new TurnEndSignal {
                        color = color
                    });
                }
            } else if (noPawnCanMove()) {
                _signalBus.Fire(new TurnEndSignal {
                    color = color
                });
            } else {
                roll = signal.roll;
            }
        } 
    }

    private void MovePawn(SelectedPawnSignal signal) {
        if (signal.color.Equals(color)) {
            int currentPosition = pawns[signal.id].currentPosition;
            int newPosition;
            if (currentPosition == -1) {
                newPosition = 0;
            } else {
                newPosition = currentPosition + roll;
            }
            
            Vector3 newVectorPosition = path[newPosition].position;

            _signalBus.Fire(
                new MovePawnSignal(
                    toPositionX: newVectorPosition.x,
                    toPositionY: newVectorPosition.y,
                    toPositionZ: newVectorPosition.z,
                    newPosition: newPosition,
                    rollCount: roll,
                    squareId: path[newPosition].id,
                    pawnID: pawns[signal.id].pawnId,
                    pawnColor: pawns[signal.id].pawnColor
                )
            );
            roll = 0;
        }
    }

    private void SetPawnValue() {
        int index = 0;
        foreach (Pawn pawn in pawns) {
            pawn.InitPawnValues(color, index);
            index++;
        }
    }

    private void SetPath() {
        int startIndex = _ludoBoard.MainPathway.FindIndex(x => x.label == start);
        int endIndex = _ludoBoard.MainPathway.FindIndex(x => x.label == end);
        for (int i = startIndex; i < _ludoBoard.MainPathway.Count; i++) {
            path.Add(_ludoBoard.MainPathway[i]);
        }
        for (int i = 0; i <= endIndex; i++) {
            path.Add(_ludoBoard.MainPathway[i]);
        }
        for(int i = 0;i < FinalPath.childCount; i++) {
            path.Add(FinalPath.GetChild(i).GetComponent<Square>());
        }
    }

    private void InitializePath((int row, int col) start, (int row, int col) next, (int row, int col) end) {
        path = new List<Square>();
        path.Add(_ludoBoard.Grid.GetCell(start.row, start.col));
        path.Add(_ludoBoard.Grid.GetCell(next.row, next.col));

        int i = next.row, j = next.col;


        while(path.Count < 52) {
            var nextCell = GetNextCell(i, j);
            Square nextCellObj = nextCell.Item1;
            if(nextCellObj != null) {
                path.Add(nextCellObj);
                i = nextCell.Item2;
                j = nextCell.Item3;
                if(i == end.row && j == end.col) {
                    break;
                }
            }
        }
    }

    private bool isEveryPawnHome() {
        foreach (Pawn pawn in pawns) {
            if(pawn.currentPosition != -1) {
                return false;
            }
        }
        return true;
    }

    private bool noPawnCanMove() {
        int count = 0;
        foreach(Pawn pawn in pawns) {
            if(pawn.canMove) {
                count++;
            }
        }
        return count == 0;
    }


    internal (Square, int , int) GetNextCell(int row, int col) {
        
        Square square;
        int nextCellI, nextCellJ;
        int i = 0, j = 0;

        //i-1, j -> i+1, j
        j = col;
        for(i = row-1; i <= row+1; i++) {

            (square, nextCellI, nextCellJ) = GetCell(i, j);
            if(square != null && !path.Contains(square)) {
                return (square, nextCellI, nextCellJ);
            } 

        }

        //i, j-1 -> i, j+1
        i = row;
        for(j = col-1; j <= col+1; j++) {

            (square, nextCellI, nextCellJ) = GetCell(i, j);
            if(square != null && !path.Contains(square)) {
                return (square, nextCellI, nextCellJ);
            } 

        }

        //i-1, j-1 -> i-1, j+1
        i = row-1;
        for(j = col-1; j <= col+1; j++) {
            
            (square, nextCellI, nextCellJ) = GetCell(i, j);
            if(square != null && !path.Contains(square)) {
                return (square, nextCellI, nextCellJ);
            }
        }

        //i+1, j-1 -> i+1, j+1
        i = row+1;
        for(j = col-1; j <= col+1; j++) {
            
            (square, nextCellI, nextCellJ) = GetCell(i, j);
            if(square != null && !path.Contains(square)) {
                return (square, nextCellI, nextCellJ);
            }
        }

        return (null, -1, -1);
    }

    internal (Square, int , int) GetCell(int i, int j) {
        Square square;

        if(i >= 0 && i < _ludoBoard.Grid.CellArray.GetLength(0) && j >= 0 && j < _ludoBoard.Grid.CellArray.GetLength(1)) {
            if(_ludoBoard.Grid.GetCell(i, j).IsPath) {
                square = _ludoBoard.Grid.GetCell(i, j);
                return (square, i, j);
            }
        }

        return (null, -1, -1);
    }
}