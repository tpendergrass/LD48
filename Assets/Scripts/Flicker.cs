using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {
    public Light lightSource;
    public Vector2 flickerRate;
    public Vector2 flickerRange;
    void Start() {
        Invoke("LightFlicker", Random.Range(flickerRate.x, flickerRate.y));
    }

    void LightFlicker() {
        lightSource.intensity = Random.Range(flickerRange.x, flickerRange.y);
        Invoke("LightFlicker", Random.Range(flickerRate.x, flickerRate.y));
    }
}
