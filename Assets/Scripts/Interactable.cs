using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    public string interactMessage = "Press E to Interact";
    public UnityEvent interactEvent;

    public void CanInteract(Interact caller) {
        caller.InteractResponse(interactMessage);
    }

    public void PerformAction() {
        interactEvent.Invoke();
    }
}
