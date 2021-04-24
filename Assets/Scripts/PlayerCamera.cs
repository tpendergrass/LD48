using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public SubmergedDetector submergedDetector;
    public GameManager manager;
    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        manager.SetWaterFog(submergedDetector.isSubmerged);
    }
}
