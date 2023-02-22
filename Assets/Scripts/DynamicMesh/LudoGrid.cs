using UnityEngine;
using Zenject;

public class LudoGrid : MonoBehaviour {

    private SignalBus _signalBus;
    private LudoBoard _ludoBoard;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
    }
    
    void Start() {

        Grid grid = new Grid(15, 15, 4.0f, transform);
        grid.Construct(_signalBus, _ludoBoard);
        grid.SetUpGrid();
        grid.HighlightPortion(6, 9, 0, 6);
        grid.HighlightPortion(0, 6, 6, 9);
        grid.HighlightPortion(9, 15, 6, 9);
        grid.HighlightPortion(6, 9, 9, 15);
    }
}
