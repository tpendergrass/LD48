using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public float fadeSpeed;
    private Color transitionColor;
    public Text target;
    void Update() {
        if(target.color.a < 1) {
            transitionColor = target.color;
            transitionColor.a += fadeSpeed;
            target.color = transitionColor;
        }
    }
}
