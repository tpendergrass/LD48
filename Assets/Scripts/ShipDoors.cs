using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDoors : MonoBehaviour {
    public Animator door1;
    public Animator door2;

    public void OpenDoors() {
        StartCoroutine(FancyOpen());
    }

    IEnumerator FancyOpen() {
        door1.enabled = true;
        yield return new WaitForSeconds(0.5f);
        door2.enabled = true;
    }
}
