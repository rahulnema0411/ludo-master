using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LudoBoard : MonoBehaviour {
    [Header("Main Path Way")]
    [SerializeField] public Transform MainPath;
    [SerializeReference] public List<Square> MainPathway;

    public Player[] players;

    public string[] TurnOrder;
    public List<string> UnassignedColors;
    public bool isMultiplayer;
    public bool host;
    public string userColor;

    private int turnIndex;
    private CustomGrid<Square> grid;

    private SignalBus _signalBus;

    public CustomGrid<Square> Grid { get => grid; set => grid = value; }

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void InitializeBoard() {
        GetPlayerPrefsData();
        InitiateGrid();
        //InitializeMainPath();
        InitializePlayers();
        SubscribeToSignals();
        turnIndex = 0;
    }

    public void ActivatePlayers() {
        string turnOrder = PlayerPrefs.GetString("turnOrder", "red yellow");
        TurnOrder = turnOrder.Split(' ');
        UnassignedColors = TurnOrder.ToList();
        foreach(Player player in players) {
            if(turnOrder.Contains(player.color.ToLower())) {
                player.gameObject.SetActive(true);
            } else {
                player.gameObject.SetActive(false);
            }
        }
    }

    public string AssignUserColor() {
        string assignedColor;
        if( UnassignedColors.Count > 0 ) {
            System.Random random = new System.Random();
            int randomVal = random.Next(0, UnassignedColors.Count);
            assignedColor = UnassignedColors[randomVal];
            UnassignedColors.RemoveAt(UnassignedColors.IndexOf(assignedColor));
        } else {
            assignedColor = "";
        }
        return assignedColor;
    }

    private void GetPlayerPrefsData() {
        host = PlayerPrefs.GetString("host", "no") == "yes" ? true : false;
        isMultiplayer = PlayerPrefs.GetString("multiplayer", "false") == "true" ? true : false;
    }

    private void SubscribeToSignals() {
        _signalBus.Subscribe<TurnEndSignal>(NextTurn);
    }

    private void NextTurn(TurnEndSignal signal) {
        int nextTurn = 0;
        int lastTurn = Array.IndexOf(TurnOrder, signal.color.ToLower());
        if(signal.previousTurnRoll != 6) {
            if(!(lastTurn == TurnOrder.Length - 1)) {
                nextTurn = lastTurn + 1;
            }
        } else {
            nextTurn = lastTurn;
        }
        _signalBus.Fire(new PlayerTurnSignal {
            color = TurnOrder[nextTurn]
        });
    }

    public void Play() {
        _signalBus.Fire(new PlayerTurnSignal { 
            color = TurnOrder[turnIndex]
        });
    }

    private void InitializePlayers() {
        foreach (Player player in players) {
            player.InitializePlayer();
        }
    }

    private void InitializeMainPath() {
        for (int i = 0; i < MainPath.childCount; i++) {
            Square square = MainPath.GetChild(i).GetComponent<Square>();
            square.SetID(i);
            MainPathway.Add(square);
        }
    }

    private void InitiateGrid() {
        grid = new CustomGrid<Square>(15, 15, 4.0f, MainPath);
        HighlightPortion(6, 9, 0, 6);
        HighlightPortion(0, 6, 6, 9);
        HighlightPortion(9, 15, 6, 9);
        HighlightPortion(6, 9, 9, 15);
    }

    public void HighlightPortion(int x1, int x2,  int y1, int y2) {

        for (int i = 0; i < grid.CellArray.GetLength(0); i++) {
            for (int j = 0; j < grid.CellArray.GetLength(1); j++) {
                Square cell = grid.CellArray[i, j].GetComponent<Square>();
                if(cell != null) {
                    if(i == 5 && j == 7 || i == 7 && j == 5 || i == 9 && j == 7 || i == 7 && j == 9) {
                        continue;
                    }
                    if((i >= x1 && i < x2) && j >= y1 && j < y2) {
                        if(i == x1 || i == x2-1 || j == y1 || j == y2-1) {
                            cell.Construct(_signalBus, this);
                            cell.SubcribeToSignals();
                            cell.GetComponent<MeshRenderer>().material = ImageHelper.instance.GetWhiteMaterial();
                            cell.IsPath = true;
                        }
                    }
                } else {
                    Debug.LogError("Square componenent attached to the cell object in : " + i + ", " + j);
                }
            }
        }
    } 
}
