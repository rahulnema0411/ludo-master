using UnityEngine;

public class Twinkle : MonoBehaviour {

    public float twinkleInterval = 0.5f;
    
    private float timeSinceLastTwinkle;

    void Update() {
        timeSinceLastTwinkle += Time.deltaTime;
        if (timeSinceLastTwinkle >= twinkleInterval) {
            gameObject.SetActive(!gameObject.activeSelf);
            timeSinceLastTwinkle = 0;
        }
    }
}