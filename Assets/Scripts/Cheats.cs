using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public Transform[] teleports;
    public GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate() {
        
        if(Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Keypad0)) {
            Debug.Log("hi");
            player.transform.position = teleports[0].position;
        }

        if(Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Keypad1)) {
            Debug.Log("bye");
            player.transform.position = teleports[1].position;
        }

        if(Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Keypad2)) {
            Debug.Log("Trapped");
            player.transform.position = teleports[2].position;
        }
    }
}
