using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public SubmergedDetector submergedDetector;
    public EnvironmentManager manager;
    public ParticleSystem waterParticles;
    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnvironmentManager>();
    }

    public void SetSubmerged(bool isSubmerged) {
        submergedDetector.isSubmerged = isSubmerged;
    }

    // Update is called once per frame
    void Update() {
        if(manager.underwater) {
            manager.SetUnderWater();
        } else {
            manager.SetAboveWater();
        }
        manager.underwater = submergedDetector.isSubmerged;
        waterParticles.gameObject.SetActive(submergedDetector.isSubmerged);
    }
}
