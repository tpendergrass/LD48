using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelManager : MonoBehaviour {
    public float[] waterLevels;
    public GameObject water;
    public int currentWaterLevel;
    private Vector3 targetWaterLevel;
    private Vector3 waterVelocity;
    public float smoothSpeed;

    void Update() {
        targetWaterLevel = water.transform.position;
        targetWaterLevel.y = waterLevels[currentWaterLevel];
        water.transform.position = Vector3.SmoothDamp(water.transform.position, targetWaterLevel, ref waterVelocity, smoothSpeed);
    }

    public void SetWaterLevel(int newWaterLevel) {
        currentWaterLevel = newWaterLevel;
    }
}
