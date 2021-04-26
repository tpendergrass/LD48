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
    public GameObject sunlight;
    public bool underwater = false;
    public GameObject player;
    public float lowFogDensity = 0.02f;
    public float highFogDensity = 0.08f;
    public Color lowFogColor;
    public Color highFogColor; 

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Update() {
        if(underwater) {
            handleFogDepth();
            return;
        }
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
    public void SetAboveGround() {
        ApplySetting(0);
    }

    public void SetBelowGround() {
        ApplySetting(1);
    }

    public void ApplySetting(int settingIndex) {
        currentIndex = settingIndex;
        RenderSettings.skybox = settings[settingIndex].skyboxMaterial;
        RenderSettings.fogColor = settings[settingIndex].fogColor;
        targetFogDensity = settings[settingIndex].fogDensity;
        sunlight.SetActive(settings[settingIndex].sunlight);
    }

    void ForceSetting(int settingIndex) {
        RenderSettings.fogColor = settings[settingIndex].fogColor;
        RenderSettings.fogDensity = settings[settingIndex].fogDensity;
        targetFogDensity = settings[settingIndex].fogDensity;
    }

    public void SetUnderWater() {
        // ForceSetting(2);
        underwater = true;
    }

    public void SetAboveWater() {
        ForceSetting(currentIndex);
        underwater = false;
    }

    void TransitionFog() {
        RenderSettings.fogDensity = Mathf.SmoothDamp(RenderSettings.fogDensity, targetFogDensity, ref fogTransitionVelocity, fogRate);
    }

    void handleFogDepth() {
        float depthRange = player.transform.position.y/-84.0f;
        RenderSettings.fogDensity = Mathf.Lerp(lowFogDensity, highFogDensity, depthRange);
        RenderSettings.fogColor = Color.Lerp(lowFogColor, highFogColor, depthRange);
    }
}
