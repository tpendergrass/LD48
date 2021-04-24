using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public GameObject body;
    public GameObject head;
    private float headRotation; //eulerAngle limits are hard when quaternions are involved...
    public Vector2 headAngleLimit;
    public float mouseSensitivity;
    public float selfRightSpeed = 3.5f;
    // Update is called once per frame

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Look() {
        float deltaXRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float deltaYRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;
        if(!head) {
            RotateAll(deltaXRotation, -deltaYRotation);
        } else {
            RotateHead(-deltaYRotation); //natural mouse movement is inverted.
            RotateBody(deltaXRotation);
        }
    }

    void RotateAll(float deltaX, float deltaY) {
        body.transform.Rotate(new Vector3(deltaY, deltaX, 0));
        MakeUpright();
    }

    void MakeUpright() {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, selfRightSpeed * Time.deltaTime);
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
