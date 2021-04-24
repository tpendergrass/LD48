using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController controller;
    public float moveSpeed;
    private Vector3 movementVector;
    public Vector3 verticalVelocity;
    public float jumpHeight;
    public float gravity = -0.1f;
    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(controller.isGrounded);
        calculateGravity();
        calculateJump();
        calculateMovementInput();
        controller.Move(movementVector * moveSpeed * Time.deltaTime);
        controller.Move(verticalVelocity);
    }

    void calculateGravity() {
        if(controller.isGrounded) {
            verticalVelocity.y = -1 * Time.deltaTime;
            return;
        }

        verticalVelocity.y += gravity  * Time.deltaTime; //check for swimmin?
    }

    void calculateMovementInput() {
        movementVector.x = 0;
        movementVector.z = 0;
        movementVector += transform.forward * (Input.GetAxis("Vertical"));
        movementVector += transform.right * (Input.GetAxis("Horizontal"));
        //prevent diagonal movement exceeding max speed
        if(movementVector.magnitude > 1) {
            movementVector = movementVector.normalized;
        }
    }

    void calculateJump() {
        if(Input.GetButtonDown("Jump") && controller.isGrounded) {
            verticalVelocity.y = jumpHeight;
        }
    }
}
