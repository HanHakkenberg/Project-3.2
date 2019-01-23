using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform cameraPivot;
    [SerializeField] Transform cameraTarget;
    [SerializeField] int camAngle;
    [SerializeField] bool borderMovement = false;
    [SerializeField] BoolReference canControl;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] int camBorderSize;

    [Header("Rotation")]
    [SerializeField] int rotationSpeed = 10;

    [Header("Zoom")]
    [SerializeField] int maxZoom = 30;
    [SerializeField] int minZoom = 10;
    [SerializeField] int startZoom = 15;
    [SerializeField] int zoomSpeed = 10;

    [Header("Position Border")]
    [SerializeField] int borderYMin, borderYMax;
    [SerializeField] int borderXMin, borderXMax;

    [Header("Switch")]
    [SerializeField] int switchMax;
    [SerializeField] int switchMin;
    [SerializeField] float switchSpeed;
    [SerializeField] GameEvent switchEvent;
    [SerializeField] GameEvent stopSwitch;

    [SerializeField] Vector3Reference camPos;
    [SerializeField] BoolReference shipFollow;

    void Awake() {
        cameraPivot.eulerAngles = new Vector3(-camAngle, 0, 0);
        cameraTarget.localPosition = new Vector3(0, startZoom, 0);
    }

    void Update() {
        if (canControl.Value) {
            UpdatePlayerControler();
        }
    }

    public void UpdatePlayerControler() {
        if (shipFollow.Value) {
            transform.SetPositionAndRotation(camPos.Value, Quaternion.Euler(UpdateCameraRotation()));
        }
        else {
            transform.SetPositionAndRotation(UpdateCameraPosition(), Quaternion.Euler(UpdateCameraRotation()));
        }
        UpdateCamZoom();
    }

    #region Camera Movement And Rotation
    Vector3 newPosition = new Vector3();

    Vector3 UpdateCameraPosition() {

        newPosition = (VerMovement() + HorMovement());

        if (Input.GetButtonDown("Vertical") && Input.GetButtonDown("Horizontal")) {
            newPosition = newPosition * (movementSpeed * Time.unscaledDeltaTime / 2) + transform.position;
        }
        else{
            newPosition = newPosition * movementSpeed * Time.unscaledDeltaTime + transform.position;
        }

        return (new Vector3(Mathf.Clamp(newPosition.x, borderXMin, borderXMax), newPosition.y, Mathf.Clamp(newPosition.z, borderYMin, borderYMax)));
    }

    //Checks If The Player Wants To Move Horizontaly
    Vector3 HorMovement() {
        if (Input.GetButton("Horizontal")) {
            if (Input.GetAxisRaw("Horizontal") > 0.1f) {
                return (transform.right);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0.1f) {
                return (-transform.right);
            }
        }
        else if (borderMovement == true) {
            if (Input.mousePosition.x >= Screen.width - camBorderSize) {
                return (transform.right);
            }
            else if (Input.mousePosition.x <= camBorderSize) {
                return (-transform.right);
            }
        }

        return (Vector3.zero);
    }

    //Checks If The Player Wants To Move Verticaly
    Vector3 VerMovement() {
        if (Input.GetButton("Vertical")) {
            if (Input.GetAxisRaw("Vertical") > 0.1f) {
                return (transform.forward);
            }
            else if (Input.GetAxisRaw("Vertical") < 0.1f){
                return (-transform.forward);
            }
        }
        else if (borderMovement == true) {
            if (Input.mousePosition.y >= Screen.height - camBorderSize) {
                return (transform.forward);
            }
            else if (Input.mousePosition.y <= camBorderSize) {
                return (-transform.forward);
            }
        }

        return (Vector3.zero);
    }

    //Claculates The New Rotation Of The Camera Parent
    Vector3 oldMousePos;
    bool rotating;

    Vector3 UpdateCameraRotation() {
        if (Input.GetButton("Fire3")) {
            if (rotating == false) {
                oldMousePos = Input.mousePosition;
                rotating = true;
            }

            if (Input.mousePosition.x - oldMousePos.x >= 0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.unscaledDeltaTime * rotationSpeed, transform.eulerAngles.z));
            }
            else if (Input.mousePosition.x - oldMousePos.x <= -0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - Time.unscaledDeltaTime * rotationSpeed, transform.eulerAngles.z));
            }
        }
        else {
            if (Input.GetAxis("Camera Rotation") >= 0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - Time.unscaledDeltaTime * rotationSpeed, transform.eulerAngles.z));
            }
            else if (Input.GetAxis("Camera Rotation") <= -0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.unscaledDeltaTime * rotationSpeed, transform.eulerAngles.z));
            }

            rotating = false;
        }

        return (transform.eulerAngles);
    }

    //Calculates The Zoom For The Camera
    void UpdateCamZoom() {
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            cameraTarget.localPosition = new Vector3(0, Mathf.Clamp(cameraTarget.localPosition.y - Input.GetAxisRaw("Mouse ScrollWheel") * Time.unscaledDeltaTime * zoomSpeed, minZoom, maxZoom), 0);
        }
        else {
            cameraTarget.localPosition = new Vector3(0, Mathf.Clamp(cameraTarget.localPosition.y - Input.GetAxisRaw("Mouse ScrollWheel Keys") * Time.unscaledDeltaTime * zoomSpeed, minZoom, maxZoom), 0);
        }
    }
    #endregion

    /// <summary>
    /// Zooms the camera to the correct possision to switch
    /// </summary>
    /// <param name="zoomIn"></param>
    /// <returns>Desides if it zoom in or out</returns>
    public void StartSwitch(bool zoomIn) {
        StartCoroutine(SwitchZoom(zoomIn, false));
    }

    public void StartShipSwitch(bool zoomIn) {
        StartCoroutine(SwitchZoom(zoomIn, true));
    }

    IEnumerator SwitchZoom(bool zoomIn, bool followShip) {
        if (zoomIn == true) {
            cameraTarget.localPosition = new Vector3(0, switchMax, 0);

            while (cameraTarget.localPosition.y >= switchMin) {
                cameraTarget.localPosition = new Vector3(0, cameraTarget.localPosition.y - Time.unscaledDeltaTime * switchSpeed, 0);
                yield return null;
            }
            canControl.Value = true;
            stopSwitch.Raise();
        }
        else {
            canControl.Value = false;

            while (cameraTarget.localPosition.y <= switchMax) {
                cameraTarget.localPosition = new Vector3(0, cameraTarget.localPosition.y + Time.unscaledDeltaTime * switchSpeed, 0);
                yield return null;
            }
            switchEvent.Raise();

            if (followShip) {
                shipFollow.Value = false;
            }
        }

    }
}