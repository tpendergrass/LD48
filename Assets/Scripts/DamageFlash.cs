using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour {
    public Image image;
    public float flashAlpha;
    private Color transitionColor;

    public float recoverSpeed;

    void Update() {
        if(image.color.a > 0) {
            transitionColor = image.color;
            transitionColor.a -= recoverSpeed;
            image.color = transitionColor;
        }
    }

    public void Flash() {
        transitionColor = image.color;
        transitionColor.a = flashAlpha;
        image.color = transitionColor;
    }
}
