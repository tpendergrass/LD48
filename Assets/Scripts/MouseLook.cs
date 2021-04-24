using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public GameObject body;
    public GameObject head;
    private float headRotation; //eulerAngle limits are hard when quaternions are involved...
    public Vector2 headAngleLimit;
    public float mouseSensitivity;
    // Update is called once per frame

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        float deltaXRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float deltaYRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;
        RotateHead(-deltaYRotation); //natural mouse movement is inverted.
        RotateBody(deltaXRotation);
    }

    void RotateBody(float delta) {
        body.transform.Rotate(new Vector3(0, delta, 0));
    }

    void RotateHead(float delta) {
        headRotation += delta;
        headRotation = Mathf.Clamp(headRotation, headAngleLimit.x, headAngleLimit.y);
        head.transform.localRotation = Quaternion.Euler(new Vector3(headRotation, 0, 0));
    }
}
