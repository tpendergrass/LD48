using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float fogRate;
    public float lowFogDensity = 0.02f;
    public float highFogDensity = 0.08f;
    public Color lowFogColor;
    public Color highFogColor; 

    private float fogTransitionVelocity;
    public GameObject player;
    public UnityEvent gameStartEvents;
    public GameObject flareUI;
    public GameObject flashlightUI;
    public GameObject depthChargeUI;
    public float DepthChargeTick = 30.0f;
    public float DepthChargeTimer;
    public bool DepthChargeArmed;
    public int DepthChargeTunnel;
    public GameObject DepthChargeCounterUI;
    public Text DepthChargeCounterText;
    public GameObject[] TunnelRubble;
    public AnimPlayTrigger[] TentacleBlocker;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        gameStartEvents.Invoke();
    }

    // Update is called once per frame
    void Update() {
        // TransitionFogDensity();
        handleFogDepth();
        handleDepthCharge();
    }

    void handleFogDepth() {
        float depthRange = player.transform.position.y/-84.0f;
        RenderSettings.fogDensity = Mathf.Lerp(lowFogDensity, highFogDensity, depthRange);
        RenderSettings.fogColor = Color.Lerp(lowFogColor, highFogColor, depthRange);
    }

    void handleDepthCharge() {
        if(!DepthChargeArmed) {
            return;
        }
        DepthChargeTimer -= Time.deltaTime;
        DepthChargeCounterText.text = "TIME REMAINING: " + DepthChargeTimer.ToString("F2");
        if(DepthChargeTimer <= 0) {
            Debug.Log("KABOOM");
            DepthChargeArmed = false;
            DepthChargeCounterUI.SetActive(false);
            TunnelRubble[DepthChargeTunnel].SetActive(true);
            ClearTentacle();
        }
    }

    void ClearTentacle() {
        TentacleBlocker[DepthChargeTunnel].PlayAnimation();
    }

    public void SetWaterFog(bool isOn) {
        RenderSettings.fog = isOn;
    }

    public void ArmDepthCharge(int tunnel) {
        DepthChargeTunnel = tunnel;
        DepthChargeTimer = 25.0f;
        DepthChargeArmed = true;
        DepthChargeCounterUI.SetActive(true);
    }

    public void EnableFlareUI() {
        flareUI.SetActive(true);
    }

    public void EnableFlashlightUI() {
        flashlightUI.SetActive(true);
    }

    public void SetDepthChargeUI(bool isVisible) {
        depthChargeUI.SetActive(isVisible);
    }

    void TransitionFogDensity() {
        // RenderSettings.fogDensity = Mathf.SmoothDamp(RenderSettings.fogDensity, fogDensity, ref fogTransitionVelocity, fogRate);
    }
}
