using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {
    public Camera subCamera;
    public Player submarine;
    public SubmergedDetector entryPoint;
    public GameObject occupant;
    
    void Update() {
        if(submarine.isControlling && Input.GetKeyDown(KeyCode.E)) {
            ExitSubmarine();
        }
    }

    public void EnterSubmarine(GameObject requester) {
        occupant = requester;
        occupant.SetActive(false);
        submarine.SetControlling(true);
        subCamera.gameObject.SetActive(true);
    }

    public void ExitSubmarine() {
        submarine.SetControlling(false);
        subCamera.gameObject.SetActive(false);
        occupant.transform.position = entryPoint.transform.position;
        if(entryPoint.isSubmerged) {
            occupant.SendMessage("StartSwimming");
        } else {
            occupant.SendMessage("StopSwimming");
        }
        occupant.SetActive(true);
        occupant = null;
    }
}
