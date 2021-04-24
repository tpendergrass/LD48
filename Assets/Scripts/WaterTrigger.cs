using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    void OnTriggerStay(Collider other) {
        Debug.Log("TRIGGERED");
        Debug.Log(other.transform.position.z - transform.position.z);
        if(other.transform.position.y > transform.position.y) {
            other.gameObject.SendMessage("StopSwimming", SendMessageOptions.DontRequireReceiver);
        } else {
            other.gameObject.SendMessage("StartSwimming", SendMessageOptions.DontRequireReceiver);
        }
    }
}
