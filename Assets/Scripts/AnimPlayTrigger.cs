using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayTrigger : MonoBehaviour {
    public Animation anim;

    public void PlayAnimation() {
        anim.Play();
    }
}
