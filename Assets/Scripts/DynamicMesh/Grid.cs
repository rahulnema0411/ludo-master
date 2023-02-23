using System.Collections.Generic;
using UnityEngine;

public class CustomGrid<T> {
    private int columns;
    private int rows;
    private int[,] gridArray;
    private Transform parentTransform;
    private float cellSizeX, cellSizeY;
    private float gridSize;
    private T[,] cellArray;
    private List<Vector2> greenCells;

    public float CellSizeX { get => cellSizeX; set => cellSizeX = value; }
    public float CellSizeY { get => cellSizeY; set => cellSizeY = value; }
    public T[,] CellArray { get => cellArray; set => cellArray = value; }

    public CustomGrid(int columns, int rows, float gridSize, Transform parentTransform) {
        this.rows = rows;
        this.columns = columns;
        this.parentTransform = parentTransform;
        this.gridSize = gridSize;
        greenCells = new List<Vector2>();
        CellSizeX = gridSize/ rows;
        CellSizeY = gridSize/ columns;
        gridArray = new int[columns, rows];
        cellArray = new T[columns, rows];
        ConstructGrid();
    }

    private void ConstructGrid() {
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                CellArray[i, j] = CreateGridCell(parentTransform, i, j, GetWorldPosition(i, j) + new Vector3(CellSizeX - gridSize, CellSizeY - gridSize) * 0.5f);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x * CellSizeX, y * CellSizeY);
    }

    private T CreateGridCell(Transform parent = null, int i = 0, int j = 0, Vector3 localPosition = default, Color? color = null) {
        GameObject gameObject = new GameObject("Cell", typeof(T));
        gameObject.AddComponent(typeof(MeshFilter));
        gameObject.AddComponent(typeof(MeshRenderer));

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = ImageHelper.instance.getQuadMesh();
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = ImageHelper.instance.GetBlackMaterial();

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale = new Vector2(CellSizeX, CellSizeY);

        return gameObject.GetComponent<T>();
    }

    
}
