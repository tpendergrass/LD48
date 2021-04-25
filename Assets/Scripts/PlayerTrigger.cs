using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour {
    public GameObject player;
    public UnityEvent triggerEvent;
    public GameObject[] triggerEnablers;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.transform.root.gameObject != player) {
            return;
        }
        triggerEvent.Invoke();
        gameObject.SetActive(false);
        foreach (GameObject item in triggerEnablers) {
            item.SetActive(true);
        }
    }
}
