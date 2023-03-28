using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LudoBoard : MonoBehaviour {
    [Header("Game Data")]
    public LudoData ludoData;

    [Header("Main Path Way")]
    [SerializeField] public Transform MainPath;
    [SerializeReference] public List<Square> MainPathway;
    [SerializeReference] public Transform CrossFade;

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
        InitializePlayers();
        SubscribeToSignals();
        ColorHomes();
        turnIndex = 0;
    }

    public void ActivatePlayers() {
        string turnOrder = ludoData.turnOrder;
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

    public void UpdateUnassignedColorList(string color) {
        if(UnassignedColors.IndexOf(color) >= 0 && UnassignedColors.IndexOf(color) < UnassignedColors.Count) {
            UnassignedColors.RemoveAt(UnassignedColors.IndexOf(color));
        }
    }

    private void GetPlayerPrefsData() {
        host = ludoData.isHost;
        isMultiplayer = ludoData.isMultiplayer;
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
        foreach (Player player in players) {
            player.SetStarCells();
        }
    }


    private void InitiateGrid() {
        grid = new CustomGrid<Square>(15, 15, 4.0f, MainPath);
        HighlightPortion(6, 9, 0, 6);
        HighlightPortion(0, 6, 6, 9);
        HighlightPortion(9, 15, 6, 9);
        HighlightPortion(6, 9, 9, 15);        
    }

    private void ColorHomes()
    {
        ColorPortion(0, 0, 6, 6, ImageHelper.instance.RedPlayerColor);
        ColorPortion(0, 9, 6, 15, ImageHelper.instance.GreenPlayerColor);
        ColorPortion(9, 9, 15, 15, ImageHelper.instance.YellowPlayerColor);
        ColorPortion(9, 0, 15, 6, ImageHelper.instance.BluePlayerColor);
        SetHomeCell();
    }

    private void SetHomeCell()
    {
        grid.CellArray[7, 7].GetComponent<SpriteRenderer>().sprite = ImageHelper.instance.HomeCell;
        grid.CellArray[7, 7].GetComponent<SpriteRenderer>().sortingOrder = 1;
        grid.CellArray[7, 7].transform.localRotation = UnityEngine.Quaternion.Euler(0f, 0f, 90f);
    }

    public void HighlightPortion(int x1, int x2,  int y1, int y2) {

        for (int i = 0; i < grid.CellArray.GetLength(0); i++) {
            for (int j = 0; j < grid.CellArray.GetLength(1); j++) {
                Square cell = grid.CellArray[i, j].GetComponent<Square>();
                if(cell != null) {
                    cell.GridPosition = new Vector2Int(i, j);
                    if(i == 5 && j == 7 || i == 7 && j == 5 || i == 9 && j == 7 || i == 7 && j == 9) {
                        cell.gameObject.GetComponent<SpriteRenderer>().sprite = ImageHelper.instance.GetCellSprite();
                        cell.IsFinalPath = true;
                        continue;
                    }
                    if((i >= x1 && i < x2) && j >= y1 && j < y2) {
                        cell.Construct(_signalBus, this);
                        cell.SubcribeToSignals();
                        cell.gameObject.GetComponent<SpriteRenderer>().sprite = ImageHelper.instance.GetCellSprite();
                        if(i == x1 || i == x2-1 || j == y1 || j == y2-1) {
                            cell.IsPath = true;
                        } else {
                            cell.IsFinalPath = true;
                        }
                    }
                } else {
                    Debug.LogError("Square componenent attached to the cell object in : " + i + ", " + j);
                }
            }
        }
    } 

    public void ColorPortion(int x1, int y1,  int x2, int y2, Color color) {
        for (int i = 0; i < grid.CellArray.GetLength(0); i++) {
            for (int j = 0; j < grid.CellArray.GetLength(1); j++) {
                Square cell = grid.CellArray[i, j].GetComponent<Square>();
                if(cell != null) {
                    if((i >= x1 && i < x2) && j >= y1 && j < y2) {
                        cell.gameObject.GetComponent<SpriteRenderer>().color = color;
                    }
                } else {
                    Debug.LogError("Square componenent attached to the cell object in : " + i + ", " + j);
                }
            }
        }
    } 
}
