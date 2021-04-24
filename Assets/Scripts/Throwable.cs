using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    public GameObject projectile;
    public float randomRotation;
    public Transform emitter;
    public float projectileVelocity;

    public void Throw() {
        GameObject clone = GameObject.Instantiate(projectile, emitter.position, emitter.rotation);
        Rigidbody rigid = clone.GetComponent<Rigidbody>();
        rigid.velocity = projectileVelocity * emitter.forward;
        rigid.AddRelativeTorque(Random.Range(0, randomRotation),Random.Range(0, randomRotation),Random.Range(0, randomRotation));
    }

    public void UseItem() {
        Throw();
    }
}
