using UnityEngine;

public class ImageHelper : MonoBehaviour {
    public static ImageHelper instance;

    public Mesh mesh; 
    public Sprite square;

    [SerializeReference] private Material blackMaterial, whiteMaterial;

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
}
