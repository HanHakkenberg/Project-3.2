using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] TransformReference mainCamera; //  ***Remove
    [SerializeField] Transform cameraPivot;
    [SerializeField] Transform cameraTarget;
    [SerializeField] int camAngle;

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

    void Start() {
        cameraPivot.eulerAngles = new Vector3(-camAngle, 0, 0);
        cameraTarget.localPosition = new Vector3(0, startZoom, 0);
        cameraTarget.localEulerAngles = new Vector3(90, 180, 180);
    }

    void Update() {
        UpdatePlayerControler();
        mainCamera.Value.SetPositionAndRotation(cameraTarget.position, cameraTarget.rotation);  //  ***Remove
    }

    public void UpdatePlayerControler() {
        transform.SetPositionAndRotation(UpdateCameraPosition(), Quaternion.Euler(UpdateCameraRotation()));
        UpdateCamZoom();
    }


    #region Camera Movement And Rotation
    Vector3 newPosition = new Vector3();

    Vector3 UpdateCameraPosition() {

        newPosition = (VerMovement() + HorMovement());

        if(newPosition.x > 0.01 && newPosition.x < -0.01 && newPosition.y > 0.01 && newPosition.y < -0.01) {
            newPosition = newPosition * (movementSpeed * Time.deltaTime / 2) + transform.position;
        }
        else {
            newPosition = newPosition * movementSpeed * Time.deltaTime + transform.position;
        }

        return (new Vector3(Mathf.Clamp(newPosition.x, borderXMin, borderXMax), newPosition.y, Mathf.Clamp(newPosition.z, borderYMin, borderYMax)));
    }

    //Checks If The Player Wants To Move Horizontaly
    Vector3 HorMovement() {

        if(Input.GetButton("Horizontal")) {
            if(Input.GetAxis("Horizontal") > 0) {
                return (transform.right);
            }
            else {
                return (-transform.right);
            }
        }
        else if(Input.mousePosition.x >= Screen.width - camBorderSize) {
            return (transform.right);
        }
        else if(Input.mousePosition.x <= camBorderSize) {
            return (-transform.right);
        }

        return (new Vector3());
    }

    //Checks If The Player Wants To Move Verticaly
    Vector3 VerMovement() {

        if(Input.GetButton("Vertical")) {
            if(Input.GetAxis("Vertical") > 0) {
                return (transform.forward);
            }
            else {
                return (-transform.forward);
            }
        }
        else if(Input.mousePosition.y >= Screen.height - camBorderSize) {
            return (transform.forward);
        }
        else if(Input.mousePosition.y <= camBorderSize) {
            return (-transform.forward);
        }

        return (new Vector3());
    }

    //Claculates The New Rotation Of The Camera Parent
    Vector3 oldMousePos;
    bool rotating;

    Vector3 UpdateCameraRotation() {

        if(Input.GetButton("Fire3")) {

            if(rotating == false) {
                oldMousePos = Input.mousePosition;
                rotating = true;
            }

            if(Input.mousePosition.x - oldMousePos.x >= 0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * rotationSpeed, transform.eulerAngles.z));
            }
            else if(Input.mousePosition.x - oldMousePos.x <= -0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - Time.deltaTime * rotationSpeed, transform.eulerAngles.z));
            }
        }
        else {
            rotating = false;

            if(Input.GetAxis("Camera Rotation") >= 0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - Time.deltaTime * rotationSpeed, transform.eulerAngles.z));
            }
            else if(Input.GetAxis("Camera Rotation") <= -0.1f) {
                return (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * rotationSpeed, transform.eulerAngles.z));

            }
        }

        return (transform.eulerAngles);
    }

    //Calculates The Zoom For The Camera
    void UpdateCamZoom() {
        if(Input.GetAxis("Mouse ScrollWheel") != 0) {
            cameraTarget.localPosition = new Vector3(0, Mathf.Clamp(cameraTarget.localPosition.y - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed, minZoom, maxZoom), 0);
        }
        else {
            cameraTarget.localPosition = new Vector3(0, Mathf.Clamp(cameraTarget.localPosition.y - Input.GetAxis("Mouse ScrollWheel Keys") * Time.deltaTime * zoomSpeed, minZoom, maxZoom), 0);
        }
    }
    #endregion
}
