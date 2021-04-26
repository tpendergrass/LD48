using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public bool isControlling;
    private CharacterController controller;
    public MouseLook mouseLook;
    public float moveSpeed;
    public GameObject mainCamera;
    public Vector3 movementVector;
    public Vector3 verticalVelocity;
    private Vector3 verticalSwimDampening;
    public float jumpHeight;
    public float gravity = -0.1f;
    public float swimGravity = 0.01f;
    public float swimDampening = 0.3f;
    public bool isSwimming = false;
    public Interact interactCheck;
    public PlayerUI playerUI;
    public GameObject flashlight;

    public int playerHealth = 4;
    public UnityEvent damageEvent;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        playerHealth *= 2; //DUMB HAX because camera collider doubles damage --FACEPALM--
    }

    // Update is called once per frame
    void Update() {
        if(isControlling) {
            mouseLook.Look();
            handleInteraction();
            handleFlashlight();
        }
        if(isSwimming) {
            handleSwimming();
        } else {
            handleWalking();
        }
    }

    void handleFlashlight() {
        if(!Input.GetKeyDown(KeyCode.F)) {
            return;
        }

        flashlight.SetActive(!flashlight.activeSelf);
        playerUI.SetFlashlight(flashlight.activeSelf);
    }

    void handleInteraction() {
        if(!Input.GetKeyDown(KeyCode.E)) {
            return;
        }

        interactCheck.PerformAction(this);
    }

    // ========= MOVEMENT ==========
    void handleSwimming() {
        calculateFloating();
        if(isControlling) {
            calculateJump();
            calculateSwimmingInput();
        }
        controller.Move(movementVector * moveSpeed * Time.deltaTime);
        controller.Move(verticalVelocity);
    }

    void handleWalking() {
        calculateGravity();
        if(isControlling) {
            calculateJump();
            calculateMovementInput();
        }
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

    public void SetControlling(bool canControl) {
        isControlling = canControl;
        if(!isControlling) {
            movementVector = Vector3.zero;
        }
    }

    public void ApplyDamage(int amount) {
        playerHealth -= 1;
        damageEvent.Invoke();
        if(isControlling && playerHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        isControlling = false;
        gameOverScreen.SetActive(true);
    }
}
