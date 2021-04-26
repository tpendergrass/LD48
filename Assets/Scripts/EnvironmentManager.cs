using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {
    public Material skyboxMaterial;
    public EnvironmentSetting[] settings;
    public int currentIndex;
    public float targetFogDensity;
    public float fogRate = 0.2f;
    private float fogTransitionVelocity;
    public bool underwater = false;
    // Start is called before the first frame update
    void Update() {
        if(Input.GetKeyDown(KeyCode.Keypad7)) {
            SetAboveGround();
        }

        if(Input.GetKeyDown(KeyCode.Keypad8)) {
            SetBelowGround();
        }

        if(Input.GetKeyDown(KeyCode.Keypad9)) {
            underwater = !underwater;
            if(underwater) {
                SetUnderWater();
            } else {
                SetAboveWater();
            }
        }

        TransitionFog();
    }

    // Update is called once per frame
    void SetAboveGround() {
        ApplySetting(0);
    }

    void SetBelowGround() {
        ApplySetting(1);
    }

    void ApplySetting(int settingIndex) {
        currentIndex = settingIndex;
        RenderSettings.skybox = settings[settingIndex].skyboxMaterial;
        RenderSettings.fogColor = settings[settingIndex].fogColor;
        targetFogDensity = settings[settingIndex].fogDensity;
    }

    void ForceSetting(int settingIndex) {
        RenderSettings.fogColor = settings[settingIndex].fogColor;
        RenderSettings.fogDensity = settings[settingIndex].fogDensity;
        targetFogDensity = settings[settingIndex].fogDensity;
    }

    void SetUnderWater() {
        ForceSetting(2);
        underwater = true;
    }

    void SetAboveWater() {
        ForceSetting(currentIndex);
        underwater = false;
    }

    void TransitionFog() {
        RenderSettings.fogDensity = Mathf.SmoothDamp(RenderSettings.fogDensity, targetFogDensity, ref fogTransitionVelocity, fogRate);
    }
}
