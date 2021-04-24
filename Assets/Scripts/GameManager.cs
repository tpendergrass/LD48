using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public float fogRate;
    public float fogDensity;
    private float fogTransitionVelocity;
    // Update is called once per frame
    void Update() {
        TransitionFogDensity();
    }

    public void SetWaterFog(bool isOn) {
        RenderSettings.fog = isOn;
    }

    void TransitionFogDensity() {
        RenderSettings.fogDensity = Mathf.SmoothDamp(RenderSettings.fogDensity, fogDensity, ref fogTransitionVelocity, fogRate);
    }
}
