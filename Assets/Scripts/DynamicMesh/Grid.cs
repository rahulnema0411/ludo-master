using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Grid {
    private int columns;
    private int rows;
    private int[,] gridArray;
    private Transform parentTransform;
    private float cellSizeX, cellSizeY;
    private float gridSize;
    private BoxCollider2D[,] boxColliderArray;
    private List<Vector2> greenCells;

    private SignalBus _signalBus;
    private LudoBoard _ludoBoard;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
    }

    public float CellSizeX { get => cellSizeX; set => cellSizeX = value; }
    public float CellSizeY { get => cellSizeY; set => cellSizeY = value; }

    public Grid(int columns, int rows, float gridSize, Transform parentTransform) {
        this.rows = rows;
        this.columns = columns;
        this.parentTransform = parentTransform;
        this.gridSize = gridSize;
        greenCells = new List<Vector2>();
        CellSizeX = gridSize/ rows;
        CellSizeY = gridSize/ columns;
        gridArray = new int[columns, rows];
        boxColliderArray = new BoxCollider2D[columns, rows];
    }

    public void SetUpGrid() {
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                boxColliderArray[i, j] = CreateColliderMesh(parentTransform, i, j, GetWorldPosition(i, j) + new Vector3(CellSizeX - gridSize, CellSizeY - gridSize) * 0.5f);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x * CellSizeX, y * CellSizeY);
    }


    public BoxCollider2D CreateColliderMesh(Transform parent = null, int i = 0, int j = 0, Vector3 localPosition = default, Color? color = null, int sortingOrder = 5000) {
        GameObject gameObject = new GameObject("Cell", typeof(BoxCollider2D));
        gameObject.AddComponent(typeof(MeshFilter));
        gameObject.AddComponent(typeof(MeshRenderer));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale = new Vector2(CellSizeX, CellSizeY);
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = ImageHelper.instance.getQuadMesh();
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = ImageHelper.instance.GetBlackMaterial();
        return boxCollider;
    }

    public void HighlightPortion(int x1, int x2,  int y1, int y2) {
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                if((i >= x1 && i < x2) && j >= y1 && j < y2) {

                    if(i == x1 || i == x2-1 || j == y1 || j == y2-1) {
                        BoxCollider2D collider2D = boxColliderArray[i, j];

                        collider2D.GetComponent<MeshRenderer>().material = ImageHelper.instance.GetWhiteMaterial();
                        collider2D.gameObject.AddComponent(typeof(Square));
                        collider2D.GetComponent<Square>().Construct(_signalBus, _ludoBoard);
                        collider2D.GetComponent<Square>().SubcribeToSignals();
                    } 

                } 
            }
        }
    }
}
