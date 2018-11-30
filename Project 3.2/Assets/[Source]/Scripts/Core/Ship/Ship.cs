using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    List<Transform> myPath = new List<Transform>();
    [SerializeField] TransformReference currentSelected;
    [SerializeField] CameraReference currentCamera;

    void OnMouseDown() {
        if(!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;
            StartCoroutine(PathUpdate());
        }
    }

    public void RemoveWaypoint(Transform toRemove) {
        int toRemoveIndex = myPath.IndexOf(toRemove);

        ObjectPooler.instance.AddToPool("Waypoint", myPath[toRemoveIndex].gameObject);
        myPath.RemoveAt(toRemoveIndex);
    }

    public void DestroyShip() {
        for(int i = 0; i < myPath.Count; i++) {
            ObjectPooler.instance.AddToPool("Waypoint", myPath[i].gameObject);
        }
    }

    IEnumerator PathUpdate() {
        StopCoroutine(PathUpdate());

        while(currentSelected.Value == transform) {

            if(Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire1")) {
                RaycastHit rayhit;
                if(Physics.Raycast(currentCamera.Value.ScreenPointToRay(Input.mousePosition), out rayhit)) {
                    GameObject newWaypoint = ObjectPooler.instance.GetFromPool("Waypoint", rayhit.point);
                }
            }
            yield return null;
        }
    }


}
