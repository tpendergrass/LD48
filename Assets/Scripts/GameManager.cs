using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public float fogRate;
    public float lowFogDensity = 0.02f;
    public float highFogDensity = 0.08f;
    public Color lowFogColor;
    public Color highFogColor; 

    private float fogTransitionVelocity;
    public GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        // TransitionFogDensity();
        handleFogDepth();
    }

    void handleFogDepth() {
        float depthRange = player.transform.position.y/-84.0f;
        RenderSettings.fogDensity = Mathf.Lerp(lowFogDensity, highFogDensity, depthRange);
        RenderSettings.fogColor = Color.Lerp(lowFogColor, highFogColor, depthRange);
    }

    public void SetWaterFog(bool isOn) {
        RenderSettings.fog = isOn;
    }

    void TransitionFogDensity() {
        // RenderSettings.fogDensity = Mathf.SmoothDamp(RenderSettings.fogDensity, fogDensity, ref fogTransitionVelocity, fogRate);
    }
}
