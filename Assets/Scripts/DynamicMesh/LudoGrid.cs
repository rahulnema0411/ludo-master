using UnityEngine;
using Zenject;

public class LudoGrid : MonoBehaviour {

    private CustomGrid<Square> grid;

    private SignalBus _signalBus;
    private LudoBoard _ludoBoard;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
    }
    
    void Start() {
        InitiateGrid();
    }

    private void InitiateGrid() {
        grid = new CustomGrid<Square>(15, 15, 4.0f, transform);
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
                    if((i >= x1 && i < x2) && j >= y1 && j < y2) {
                        if(i == x1 || i == x2-1 || j == y1 || j == y2-1) {
                            cell.Construct(_signalBus, _ludoBoard);
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
