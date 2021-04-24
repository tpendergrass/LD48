using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    public Ray raycast;
    public float distance;
    private RaycastHit hit;
    public GameObject currentTarget;
    public PlayerUI playerUI;

    public void Update() {
        raycast.origin = transform.position;
        raycast.direction = transform.forward;
        if(Physics.Raycast(raycast, out hit, distance)) {
            if(hit.collider.transform.root.gameObject == currentTarget) {
                return;
            }
            playerUI.HideInteractionPrompt();
            currentTarget = hit.collider.transform.root.gameObject;
            CheckIfCanInteract();
            
        } else {
            currentTarget = null;
            playerUI.HideInteractionPrompt();
        }

    }

    void CheckIfCanInteract() {
        currentTarget.SendMessage("CanInteract", this, SendMessageOptions.DontRequireReceiver);
    }

    public void InteractResponse(string message) {
        playerUI.ShowInteractionPrompt(message);
    }

    public void PerformAction(Player player) {
        if(!currentTarget) {
            return;
        }
        currentTarget.SendMessage("PerformAction", player, SendMessageOptions.DontRequireReceiver);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (transform.forward * distance) + transform.position);
    }
}
