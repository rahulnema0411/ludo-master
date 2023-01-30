using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private int columns;
    private int rows;
    private int[,] gridArray;
    private Transform parentTransform;
    private float cellSizeX, cellSizeY;
    private float gridSize;
    private BoxCollider2D[,] boxColliderArray;
    private List<Vector2> greenCells;

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
                
                // Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);//vertical
                // Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);//horizontal
            }
        }
        
        // Debug.DrawLine(GetWorldPosition(0, rows), GetWorldPosition(columns, rows), Color.white, 100f);
        // Debug.DrawLine(GetWorldPosition(columns, 0), GetWorldPosition(columns, rows), Color.white, 100f);
        
        // CreateGridBoundaries(parentTransform, new Vector3(-CellSizeX/2, gridSize/2), CellSizeX, gridSize);
        // CreateGridBoundaries(parentTransform, new Vector3(gridSize + CellSizeX/2, gridSize/2), CellSizeX, gridSize);
        
        // CreateGridBoundaries(parentTransform, new Vector3(gridSize/2, -CellSizeY/2), gridSize, CellSizeY);
        // CreateGridBoundaries(parentTransform, new Vector3(gridSize/2, gridSize + CellSizeY/2), gridSize, CellSizeY);
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x * CellSizeX, y * CellSizeY);
    }

    public void SetGridValue(int i, int j) {
        gridArray[i, j] = 1;
        greenCells.Add(new Vector2(i, j));
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

    public BoxCollider2D CreateGridBoundaries(Transform parent = null, Vector3 localPosition = default, float sizeX = 1f, float sizeY = 1f, Color? color = null, int sortingOrder = 5000) {
        GameObject gameObject = new GameObject("Boundary", typeof(BoxCollider2D));
        gameObject.AddComponent(typeof(MeshFilter));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale = new Vector2(sizeX, sizeY);
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = ImageHelper.instance.getQuadMesh();
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        return boxCollider;
    }

    public Vector3 GetRandomGreenCellPosition() {
        Vector2 random = greenCells[UnityEngine.Random.Range(0, greenCells.Count - 1)];
        Vector3 position =  GetWorldPosition((int)random.x, (int)random.y) + new Vector3(CellSizeX / 2, CellSizeY / 2);
        return position;
    }

    public void HighlightPortion(int x1, int x2,  int y1, int y2) {
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                if((i >= x1 && i < x2) && j >= y1 && j < y2) {
                    boxColliderArray[i, j].GetComponent<MeshRenderer>().material = ImageHelper.instance.GetWhiteMaterial();
                } 
            }
        }
    }
}
