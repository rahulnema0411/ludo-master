using UnityEngine;

public class LudoGrid : MonoBehaviour {
    
    void Start() {

        Grid grid = new Grid(15, 15, 4.0f, transform);
        grid.SetUpGrid();

        grid.HighlightPortion(6, 9, 0, 6);
        grid.HighlightPortion(0, 6, 6, 9);
        grid.HighlightPortion(9, 15, 6, 9);
        grid.HighlightPortion(6, 9, 9, 15);
    }
}
