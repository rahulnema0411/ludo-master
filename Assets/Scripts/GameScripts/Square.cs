using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Square : MonoBehaviour
{
    public Vector3 position;
    [SerializeField] private Vector2Int gridPosition;
    public string label = "";
    [SerializeField] private bool isPath = false;
    [SerializeField] private bool isFinalPath = false;
    public int id = 0;
    public List<Pawn> pawnsOnThisSquare;

    private SignalBus _signalBus;
    private LudoBoard _ludoBoard;

    public bool IsPath { get => isPath; set { 
                isFinalPath = !value;
                isPath = value;
            } 
        }
    public bool IsFinalPath { get => isFinalPath; set { 
                isFinalPath = value;
                isPath = !value;
            } 
        }

    public Vector2Int GridPosition { get => gridPosition; set => gridPosition = value; }

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
        SetID();
    }

    public void SubcribeToSignals() {
        _signalBus.Subscribe<MovePawnSignal>(UpdatePawnsOnThisSquare);
        _signalBus.Subscribe<TurnEndSignal>(CheckForPawnKills);
    }

    private void UpdatePawnsOnThisSquare(MovePawnSignal signalData) {
        if(signalData.squareId == id) {
            Pawn pawn = FindPawn(signalData);
            if(pawn != null) {
                pawnsOnThisSquare.Add(pawn);
            }
        } else {
            if(pawnsOnThisSquare.Count > 0) {
                Pawn pawn = pawnsOnThisSquare.Find(x => x.pawnId == signalData.pawnID && x.pawnColor == signalData.pawnColor);
                if(pawn != null) {
                    pawnsOnThisSquare.Remove(pawn);
                }
            }
        }
    }

    private Pawn FindPawn(MovePawnSignal signalData) {
        Pawn pawn = null;
        Player player = null;
        foreach (Player p in _ludoBoard.players) {
            if (signalData.pawnColor.ToLower().Equals(p.color.ToLower())) {
                player = p;
            }
        }
        if (player != null) {
            foreach (Pawn p in player.pawns) {
                if (signalData.pawnID == p.pawnId) {
                    pawn = p;
                }
            }
        } else {
            Debug.LogError("Player not found");
        }

        return pawn;
    }

    private void CheckForPawnKills(TurnEndSignal signalData) {
        StartCoroutine(CheckAndKillPawn(signalData));
    }

    void Start() {
        position = transform.position;
        this.pawnsOnThisSquare = new List<Pawn>();
    }

    public void SetID() {
        this.id = gameObject.GetInstanceID();
    }

    private IEnumerator CheckAndKillPawn(TurnEndSignal signalData) {
        yield return new WaitForSeconds(0.25f);
        if (signalData.squareId == id && pawnsOnThisSquare.Count > 1 && !label.ToLower().Contains("star")) {
            foreach(Pawn pawn in pawnsOnThisSquare) {
                if(pawn.pawnColor != signalData.pawnColor) {
                    _signalBus.Fire(new KillPawnSignal { 
                        pawnColor = pawn.pawnColor,
                        pawnId = pawn.pawnId
                    });
                    _signalBus.Fire(new UpdatePlayerPoints{
                        color = signalData.color,
                        points = 20
                    });
                }
            }
        }
    }
}
