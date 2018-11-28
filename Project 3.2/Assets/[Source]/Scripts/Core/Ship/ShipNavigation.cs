using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipNavigation : MonoBehaviour {
    [SerializeField] TransformReference mainCamera;
    [SerializeField] Ship currentShip;
    [SerializeField] Vector3Reference wayPointToRemove;

    void Update() {
        
    }

    public IEnumerator DragCheck() {

        StopCoroutine(DragCheck());

        while(currentShip != null && Input.GetButton("Fire1")) {

            UpdateWayPointPosition();
            yield return null;
        }
    }

    void UpdateWayPointPosition() {

    }

    public void RemoveWaypoint() {
        if(currentShip != null){
            currentShip.myPath.Remove(wayPointToRemove.Value);
        }
    }
}
