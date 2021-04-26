using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour {
    public float fadeSpeed;
    private Color transitionColor;
    public Image target;
    void Update() {
        if(target.color.a < 1) {
            transitionColor = target.color;
            transitionColor.a += fadeSpeed;
            target.color = transitionColor;
        }
    }
}
