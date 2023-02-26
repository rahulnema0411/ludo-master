using UnityEngine;

public class TestMesh : MonoBehaviour
{
    [SerializeReference] private MeshFilter meshFilter;
    
    void Start() {
        Mesh mesh = new Mesh();
        
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0f, 0f);
        vertices[1] = new Vector3(0f, 100f);
        vertices[2] = new Vector3(100f, 100f);

        foreach(int i in triangles) {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }
}
