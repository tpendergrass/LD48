using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController controller;
    public float moveSpeed;
    private Vector3 movementVector;
    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        movementVector = Vector3.zero;
        calculateMovementInput();
        calculateGravity();
        controller.Move(movementVector);
    }

    void calculateGravity() {
        if(controller.isGrounded) {
            return;
        }

        movementVector.y = Physics.gravity.y * Time.deltaTime; //check for swimmin?
    }

    void calculateMovementInput() {
        movementVector += transform.forward * (Input.GetAxis("Vertical"));
        movementVector += transform.right * (Input.GetAxis("Horizontal"));
        //prevent diagonal movement exceeding max speed
        if(movementVector.magnitude > 1) {
            movementVector = movementVector.normalized * moveSpeed * Time.deltaTime;
        } else {
            movementVector *= moveSpeed * Time.deltaTime;
        }
    }
}
