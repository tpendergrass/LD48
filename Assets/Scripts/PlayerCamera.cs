using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public SubmergedDetector submergedDetector;
    public GameManager manager;
    public ParticleSystem waterParticles;
    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void SetSubmerged(bool isSubmerged) {
        submergedDetector.isSubmerged = isSubmerged;
    }

    // Update is called once per frame
    void Update() {
        manager.SetWaterFog(submergedDetector.isSubmerged);
        waterParticles.gameObject.SetActive(submergedDetector.isSubmerged);
    }
}
