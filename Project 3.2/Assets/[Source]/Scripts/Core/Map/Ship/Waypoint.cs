using UnityEngine;

public class Waypoint : MonoBehaviour {
    Ship myShip;

    [SerializeField] TransformReference currentlySelected;
    [SerializeField] LayerMaskReference Waypointlayermask;
    [SerializeField] TransformReference currentCamera;
    [SerializeField] Collider myCollider;

    void OnEnable() {
        myShip = currentlySelected.Value.GetComponentInParent<Ship>();
    }

    void OnMouseOver() {
        if (myShip.transform == currentlySelected.Value && Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire2")) {
            gameObject.SetActive(false);
        }
    }

    RaycastHit rayhit;

    void OnMouseDrag() {
        myCollider.enabled = false;

        if (myShip.transform == currentlySelected.Value && Input.GetButton("Waypoint Interact") && Input.GetButton("Fire1") && Physics.Raycast(currentCamera.Value.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out rayhit, Waypointlayermask.Value) && rayhit.collider.CompareTag("Map")) {
            myCollider.enabled = false;
            transform.position = rayhit.point + new Vector3(0, 0.1f);
            if (myShip.myPath[0] == transform) {
                myShip.SetDestinationn(transform);
            }
        }

        myCollider.enabled = true;
    }

    void OnDisable() {
        if (myShip != null) {
            myShip.RemoveWaypoint(transform);
            ObjectPooler.instance.AddToPool("Waypoint", gameObject);
        }
    }
}