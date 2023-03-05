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
                CellArray[i, j] = CreateGridCell(parentTransform, GetWorldPosition(i, j) + new Vector3(CellSizeX - gridSize, CellSizeY - gridSize) * 0.5f);
            }
        }
        CreateGridBorder(parentTransform, new Vector3(-gridSize/2, 0), 0.01f, gridSize, Color.black);
        CreateGridBorder(parentTransform, new Vector3(gridSize/2, 0), 0.01f, gridSize, Color.black);
        CreateGridBorder(parentTransform, new Vector3(0, -gridSize/2), gridSize, 0.01f, Color.black);
        CreateGridBorder(parentTransform, new Vector3(0, gridSize/2), gridSize, 0.01f, Color.black);
        CreateGridBorder(parentTransform, new Vector3(-gridSize/10, 0), 0.01f, gridSize, Color.black);
        CreateGridBorder(parentTransform, new Vector3(gridSize/10, 0), 0.01f, gridSize, Color.black);
        CreateGridBorder(parentTransform, new Vector3(0, -gridSize/10), gridSize, 0.01f, Color.black);
        CreateGridBorder(parentTransform, new Vector3(0, gridSize/10), gridSize, 0.01f, Color.black);

        CreateCell("BoardBackground", parentTransform, Vector3.zero, new Vector2(gridSize + 0.2f, gridSize + 0.2f), -1, Color.white);
        CreateCell("BoardBackgroundBorder", parentTransform, Vector3.zero, new Vector2(gridSize + 0.22f, gridSize + 0.22f), -2, Color.black);
        CreateCell("BoardShadow", parentTransform, Vector3.zero - new Vector3(-0.1f, 0.1f), new Vector2(gridSize + 0.2f, gridSize + 0.2f), -3, Color.black, 0.3f);
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x * CellSizeX, y * CellSizeY);
    }

    private T CreateGridCell(Transform parent, Vector3 localPosition) {
        GameObject gameObject = new GameObject("Cell", typeof(T));
        
        gameObject.AddComponent(typeof(SpriteRenderer));
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ImageHelper.instance.GetCellWithoutBorderSprite();

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale = new Vector2(CellSizeX, CellSizeY);

        return gameObject.GetComponent<T>();
    }

    private void CreateCell(string name, Transform parent, Vector3 localPosition, Vector2 size, int orderInLayer, Color color, float alpha = 1f) {
        GameObject gameObject = new GameObject(name);
        
        gameObject.AddComponent(typeof(SpriteRenderer));
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ImageHelper.instance.GetCellWithoutBorderSprite();
        spriteRenderer.sortingOrder = orderInLayer;
        color.a = alpha;
        spriteRenderer.color = color;

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale = size;
    }

    private void CreateGridBorder(Transform parent, Vector3 localPositionStart, float width, float height, Color color) {
        GameObject gameObject = new GameObject("Border");

        gameObject.AddComponent(typeof(SpriteRenderer));
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ImageHelper.instance.GetCellWithoutBorderSprite();
        spriteRenderer.color  = color;
        spriteRenderer.sortingOrder = 1;

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPositionStart;
        transform.localScale = new Vector2(width, height);
    }

    public T GetCell(int row, int col) {
        return CellArray[row, col];
    }
}
