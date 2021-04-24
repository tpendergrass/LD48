using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour {
    void OnTriggerStay(Collider other) {
        if(other.transform.position.y > transform.position.y) {
            other.gameObject.SendMessage("StopSwimming", SendMessageOptions.DontRequireReceiver);
        } else {
            other.gameObject.SendMessage("StartSwimming", SendMessageOptions.DontRequireReceiver);
        }
    }
}
