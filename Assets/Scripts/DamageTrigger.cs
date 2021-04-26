using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {
    public int damage = 34;
    public float persistTime = 0.5f;

    void OnTriggerEnter(Collider other) {
        other.SendMessageUpwards("ApplyDamage", damage);
    }

    void DisableSelf() {
        gameObject.SetActive(false);
    }

    void OnEnable() {
        Invoke("DisableSelf", persistTime);
    }
}
