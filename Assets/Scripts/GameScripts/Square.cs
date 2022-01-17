using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector3 position;
    public string label = "";
    
    // Start is called before the first frame update
    void Start() {
        position = transform.position;
    }
}
