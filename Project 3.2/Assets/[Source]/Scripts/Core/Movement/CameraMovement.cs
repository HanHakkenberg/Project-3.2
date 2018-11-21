using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] TrackedReference cameraPos;
    [SerializeField] float movementSpeed = 10, rotationSpeed = 10;
    [SerializeField] float borderSize;
    [SerializeField] float cameraAngle;
    [SerializeField] float cameraDistance;

    void Update() {
        UpdateCameraPosition();
    }

    float newMoveSpeed = 0;
    Vector3 newPosition = new Vector3();

    void UpdateCameraPosition() {
        newPosition = new Vector3();
        newMoveSpeed = Time.deltaTime * movementSpeed;

        if(Input.mousePosition.y >= Screen.height + borderSize || Input.GetAxis("Vertical") >= 0.1f) {
            newPosition = transform.forward * newMoveSpeed;
        }
        else if(Input.mousePosition.y <= borderSize || Input.GetAxis("Vertical") <= 0.1f) {
            newPosition = -transform.forward * newMoveSpeed;
        }

        if(Input.mousePosition.x >= Screen.width - borderSize || Input.GetAxis("Horizontal") >= 0.1f) {
            newPosition = transform.right * newMoveSpeed;
        }
        else if(Input.mousePosition.x <=  borderSize || Input.GetAxis("Horizontal") <= 0.1f) {
            newPosition = -transform.right * newMoveSpeed;
        }

        transform.position = newPosition + transform.position;
    }
}
