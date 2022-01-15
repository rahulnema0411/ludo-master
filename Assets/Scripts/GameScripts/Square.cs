using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector3 _position;
    public string label = "";
    
    // Start is called before the first frame update
    void Start() {
        _position = transform.position;
    }
}
