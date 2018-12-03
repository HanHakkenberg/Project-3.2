using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public List<Transform> myPath = new List<Transform>();
    [SerializeField] TransformReference currentSelected;
    [SerializeField] CameraReference currentCamera;

    [Header("Spotting")]
    [SerializeField] int SpottingRefreshTimer;
    [SerializeField] int spottingSphereSize;
    [SerializeField] LayerMask spottingMask;

    void Start() {
        StartCoroutine(IslandCheck());
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
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
        for (int i = 0; i < myPath.Count; i++) {
            ObjectPooler.instance.AddToPool("Waypoint", myPath[i].gameObject);
        }
    }

    IEnumerator PathUpdate() {
        StopCoroutine(PathUpdate());

        while (currentSelected.Value == transform) {

            if (Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire1")) {
                RaycastHit rayhit;
                if (Physics.Raycast(currentCamera.Value.ScreenPointToRay(Input.mousePosition), out rayhit) && rayhit.collider.CompareTag("Map")) {
                    GameObject newWaypoint = ObjectPooler.instance.GetFromPool("Waypoint", rayhit.point + new Vector3(0, 0.1f, 0));
                    myPath.Add(newWaypoint.transform);
                }
            }
            yield return null;
        }
    }

    IEnumerator IslandCheck() {
        while (true) {
            Collider[] spottedObjects = Physics.OverlapSphere(transform.position, spottingSphereSize, spottingMask);

            if (spottedObjects.Length > 0) {
                for (int i = 0; i < spottedObjects.Length; i++) {
                    spottedObjects[i].transform.parent.GetChild(0).gameObject.SetActive(true);
                }
            }

            yield return new WaitForSeconds(SpottingRefreshTimer);
        }
    }

    void SpottingUI(){

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottingSphereSize);
    }
}