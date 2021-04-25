using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDetector : MonoBehaviour {
    public bool detectPlayer;
    public GameObject target;
    public UnityEvent detectEvent;
    public UnityEvent lossEvent;
    // Start is called before the first frame update
    void Start() {
        if(detectPlayer) {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void OnTriggerEnter(Collider other) {
        if(target) {
            if(other.transform.root.gameObject != target) {
                return;
            }
        }

        detectEvent.Invoke();
    }

    public void SendSelfMessage(string message) {
        gameObject.SendMessageUpwards(message, target);
    }

    void OnTriggerExit(Collider other) {
        if(target) {
            if(other.transform.root.gameObject != target) {
                return;
            }
        }

        Debug.Log("Lost it");
        lossEvent.Invoke();
    }
}
