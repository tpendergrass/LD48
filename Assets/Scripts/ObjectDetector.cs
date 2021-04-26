using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDetector : MonoBehaviour {
    public bool detectPlayer;
    private GameObject playerTarget;
    public GameObject target;
    public string limitTag;
    public UnityEvent detectEvent;
    public UnityEvent lossEvent;
    // Start is called before the first frame update
    void Start() {
        if(detectPlayer) {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void OnTriggerEnter(Collider other) {
        if(playerTarget) {
            if(other.transform.root.gameObject != playerTarget) {
                return;
            }
        } else if(limitTag != "") {
            if(other.transform.root.tag != limitTag) {
                Debug.Log(other.transform.root.tag);
                return;
            }
        } else {
            target = other.transform.root.gameObject;
        }

        detectEvent.Invoke();
    }

    public void SendSelfMessage(string message) {
        gameObject.SendMessageUpwards(message, target);
    }

    void OnTriggerExit(Collider other) {
        if(playerTarget) {
            if(other.transform.root.gameObject != playerTarget) {
                return;
            }
        }

        Debug.Log("Lost it");
        lossEvent.Invoke();
    }
}
