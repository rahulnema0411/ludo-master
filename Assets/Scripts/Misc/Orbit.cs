using UnityEngine;

public class Orbit : MonoBehaviour {
    /* speed of orbit (in degrees/second) */
    public float speed;

    public void Update() {
        
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
        
    }
}
