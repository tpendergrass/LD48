using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController controller;
    public float moveSpeed;
    public Camera mainCamera;
    public Vector3 movementVector;
    public Vector3 verticalVelocity;
    private Vector3 verticalSwimDampening;
    public float jumpHeight;
    public float gravity = -0.1f;
    public float swimGravity = 0.01f;
    public float swimDampening = 0.3f;
    public bool isSwimming = false;
    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if(isSwimming) {
            handleSwimming();
        } else {
            handleWalking();
        }
    }
    // ========= MOVEMENT ==========
    void handleSwimming() {
        calculateFloating();
        calculateJump();
        calculateSwimmingInput();
        controller.Move(movementVector * moveSpeed * Time.deltaTime);
        controller.Move(verticalVelocity);
    }

    void handleWalking() {
        calculateGravity();
        calculateJump();
        calculateMovementInput();
        controller.Move(movementVector * moveSpeed * Time.deltaTime);
        controller.Move(verticalVelocity);
    }

    void calculateFloating() {
        if(movementVector.y != 0) {
            verticalVelocity.y = movementVector.y * Time.deltaTime;
            movementVector.y = 0; // transfer vertical movement from movement to vertical velocity.
        }
        verticalVelocity.y += swimGravity * Time.deltaTime;
        verticalVelocity = Vector3.SmoothDamp(verticalVelocity, Vector3.zero, ref verticalSwimDampening, swimDampening);
    }

    void calculateSwimmingInput() {
        movementVector.x = 0;
        movementVector.z = 0;
        movementVector += mainCamera.transform.forward * (Input.GetAxis("Vertical"));
        movementVector += transform.right * (Input.GetAxis("Horizontal"));
        //prevent diagonal movement exceeding max speed
        if(movementVector.magnitude > 1) {
            movementVector = movementVector.normalized;
        }
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

    // ============== Triggered Behavior ===========
    public void StartSwimming() {
        isSwimming = true;
    }

    public void StopSwimming() {
        isSwimming = false;
    }
}
