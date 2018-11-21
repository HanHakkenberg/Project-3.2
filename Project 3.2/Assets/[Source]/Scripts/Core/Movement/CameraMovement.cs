using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform cameraPivot;
    [SerializeField] Transform mainCamera;
    [SerializeField] int camAngle;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] int camBorderSize;

    [Header("Rotation")]
    [SerializeField] GameEvent startRotating;
    [SerializeField] GameEvent StopRotating;
    [SerializeField] int rotationSpeed = 10;

    [Header("Zoom")]
    [SerializeField] int maxZoom = 30;
    [SerializeField] int minZoom = 10;
    [SerializeField] int startZoom = 15;
    [SerializeField] int zoomSpeed = 10;


    [Header("Position Border")]
    [SerializeField] int borderYMin, borderYMax;
    [SerializeField] int borderXMin, borderXMax;

    void Start() {
        cameraPivot.eulerAngles = new Vector3(-camAngle, 0, 0);
        mainCamera.localPosition = new Vector3(0, startZoom, 0);
    }

    void Update() {
        transform.SetPositionAndRotation(UpdateCameraPosition(), Quaternion.Euler(UpdateCameraRotation()));
        UpdateCamZoom();
    }


    #region Camera Movement And Rotation
    float newMoveSpeed = 0;
    Vector3 newPosition = new Vector3();

    Vector3 UpdateCameraPosition() {
        newPosition = new Vector3();
        newMoveSpeed = Time.deltaTime * movementSpeed;

        if(Input.mousePosition.y >= Screen.height - camBorderSize || Input.GetAxis("Vertical") >= 0.1f) {
            newPosition += transform.forward * newMoveSpeed;
        }
        else if(Input.mousePosition.y <= camBorderSize || Input.GetAxis("Vertical") <= -0.1f) {
            newPosition += -transform.forward * newMoveSpeed;
        }

        if(Input.mousePosition.x >= Screen.width - camBorderSize || Input.GetAxis("Horizontal") >= 0.1f) {
            newPosition += transform.right * newMoveSpeed;
        }
        else if(Input.mousePosition.x <= camBorderSize || Input.GetAxis("Horizontal") <= -0.1f) {
            newPosition += -transform.right * newMoveSpeed;
        }

        newPosition += transform.position;
        return (new Vector3(Mathf.Clamp(newPosition.x, borderXMin, borderXMax), newPosition.y, Mathf.Clamp(newPosition.z, borderYMin, borderYMax)));
    }

    void UpdateCamZoom() {
        mainCamera.localPosition = new Vector3(0, Mathf.Clamp(mainCamera.localPosition.y - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed, minZoom, maxZoom),0);
    }

    Vector3 UpdateCameraRotation() {

        if(Input.GetButton("Fire3")) {
            startRotating.Raise();

            if(Input.GetAxis("Mouse X") >= 0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * rotationSpeed, transform.eulerAngles.z));
            }
            else if(Input.GetAxis("Mouse X") <= -0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - Time.deltaTime * rotationSpeed, transform.eulerAngles.z));
            }
        }
        else if(Input.GetAxis("Camera Rotation") >= 0.1f) {
            return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - Time.deltaTime * rotationSpeed, transform.eulerAngles.z));
        }
        else if(Input.GetAxis("Camera Rotation") <= -0.1f) {
            return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * rotationSpeed, transform.eulerAngles.z));

        }
        else {
            StopRotating.Raise();
        }

        return (transform.eulerAngles);
    }
    #endregion
}
