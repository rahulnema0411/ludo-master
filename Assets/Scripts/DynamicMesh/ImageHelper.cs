using UnityEngine;

public class ImageHelper : MonoBehaviour {
    public static ImageHelper instance;

    public Mesh mesh; 
    public Sprite square;

    [SerializeReference] private Material blackMaterial, whiteMaterial, 
    redMaterial, greenMaterial, blueMaterial, yellowMaterial, 
    pastelRedMaterial, pastelBlueMaterial, pastelYellowMaterial, pastelGreenMaterial;

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
}
