using UnityEngine;

public class Waypoint : MonoBehaviour {
    Ship myShip;
    [SerializeField] TransformReference currentlySelected;
    [SerializeField] LayerMaskReference Waypointlayermask;
    [SerializeField] CameraReference currentCamera;

    void Awake() {
        myShip = currentlySelected.Value.GetComponentInParent<Ship>();
    }

    void OnMouseDown() {
        if(Input.GetButton("Waypoint Interaction") && Input.GetButtonDown("Fire2")) {
            myShip.RemoveWaypoint(transform);
        }
    }

    RaycastHit rayhit;

    void OnMouseDrag() {
        if(Input.GetButton("Waypoint Interaction") && Input.GetButton("Fire2") && Physics.Raycast(Input.mousePosition, currentCamera.Value.transform.eulerAngles, out rayhit, Waypointlayermask.Value)) {
            transform.position = rayhit.point;
        }
        else {
            Debug.Log("Can't Hit");
        }
    }
}
