using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimStep : MonoBehaviour {
    public Animator anim;
    // Start is called before the first frame update
    void Start() {
        anim.Update(Random.Range(0, 100)); //prevent animation syncing at spawn.
    }
}
