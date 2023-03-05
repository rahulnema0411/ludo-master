using UnityEngine;

public class ImageHelper : MonoBehaviour {
    public static ImageHelper instance;

    [SerializeReference] private Mesh mesh; 
    [SerializeReference] private Sprite square;
    [SerializeReference] private Material blackMaterial, whiteMaterial, 
    redMaterial, greenMaterial, blueMaterial, yellowMaterial, 
    pastelRedMaterial, pastelBlueMaterial, pastelYellowMaterial, pastelGreenMaterial;
    [SerializeReference] private Sprite cell, cellWithoutBorder;
    [SerializeReference] private Color redPlayerColor, bluePlayerColor, greenPlayerColor, yellowPlayerColor;

    public Color RedPlayerColor { get => redPlayerColor; set => redPlayerColor = value; }
    public Color BluePlayerColor { get => bluePlayerColor; set => bluePlayerColor = value; }
    public Color GreenPlayerColor { get => greenPlayerColor; set => greenPlayerColor = value; }
    public Color YellowPlayerColor { get => yellowPlayerColor; set => yellowPlayerColor = value; }
    public Sprite Square { get => square; set => square = value; }
    public Mesh Mesh { get => mesh; set => mesh = value; }
    public Material WhiteMaterial { get => whiteMaterial; set => whiteMaterial = value; }

    private void Awake() {
        instance = this;
    }

    public Sprite getSquare() {
        return square;
    }

    public Mesh getQuadMesh() {
        return mesh;
    }

    public Material GetBlackMaterial() {
        return blackMaterial;
    }

    public Material GetWhiteMaterial() {
        return whiteMaterial;
    }

    public Material GetRedMaterial() {
        return redMaterial;
    }

    public Material GetMaterialOfColor(string color) {
        switch(color.ToUpper()) {
            case "RED" :
                return redMaterial;
            case "BLUE" :
                return blueMaterial;
            case "YELLOW" :
                return yellowMaterial;
            case "GREEN" :
                return greenMaterial;
            case "PASTELRED" :
                return pastelRedMaterial;
            case "PASTELBLUE" :
                return pastelBlueMaterial;
            case "PASTELYELLOW" :
                return pastelYellowMaterial;
            case "PASTELGREEN" :
                return pastelGreenMaterial;
            case "WHITE" :
                return whiteMaterial;
            case "BLACK" :
                return blackMaterial;
        }
        
        return whiteMaterial;
    }

    public Sprite GetCellSprite() {
        return cell;
    }
    public Sprite GetCellWithoutBorderSprite() {
        return cellWithoutBorder;
    }
}
