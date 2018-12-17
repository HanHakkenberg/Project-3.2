using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour {
    [SerializeField] TransformReference currentSelected;
    [SerializeField] TransformReference currentCamera;

    [Header("Path")]
    bool stopIt = false;
    [SerializeField] int lineWith;
    List<Transform> myPath = new List<Transform>();

    [Header("Spotting")]
    [SerializeField] int SpottingRefreshTimer;
    [SerializeField] int spottingSphereSize;
    [SerializeField] LayerMask spottingMask;
    bool isUpdating;

    NavMeshAgent myAgent;
    LineRenderer waypointLines;

    void Awake() {
        waypointLines = GetComponent<LineRenderer>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        waypointLines.startWidth = lineWith;
        waypointLines.endWidth = lineWith;
        StartCoroutine(IslandCheck());
    }

    void Update() {
        if (myPath.Count > 0) {
            if (Vector3.Distance(transform.position, myPath[0].position) < 2) {
                myPath[0].gameObject.SetActive(false);
                UpdateWaypointPath(false);
            }
            else {
                UpdateWaypointPath(true);
            }
        }
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;
            StartCoroutine(PathUpdate());
        }
    }

    /// <summary>
    /// Removes the given waypoint.
    /// </summary>
    /// <param name="toRemove">Waypoint to remove </param>
    public void RemoveWaypoint(Transform toRemove) {
        int toRemoveIndex = myPath.IndexOf(toRemove);

        myPath.RemoveAt(toRemoveIndex);
        waypointLines.positionCount = myPath.Count + 1;
        UpdateWaypointPath(false);

        SetDestination();
    }

    /// <summary>
    /// Adding a waypoint to the path of the ship.
    /// </summary>
    /// <param name="toAdd">Transform to add</param>
    public void AddWaypoint(Transform toAdd) {
        myPath.Add(toAdd);
        waypointLines.positionCount = myPath.Count + 1;
        UpdateWaypointPath(false);
    }

    /// <summary>
    /// Removes the waypoints.
    /// </summary>
    public void DestroyShip() {
        for (int i = 0; i < myPath.Count; i++) {
            ObjectPooler.instance.AddToPool("Waypoint", myPath[i].gameObject);
        }
    }

    /// <summary>
    /// Updates the waypoints when removed or moved.
    /// </summary>
    /// <param name="toUpdate"> Waypoint to update</param>
    public void UpdateWaypoint() {
        UpdateWaypointPath(false);
    }

    //Updates the path by adding, moving or removing a waypoint
    IEnumerator PathUpdate() {
        if (isUpdating == false) {
            isUpdating = true;

            while (currentSelected.Value == transform) {

                RaycastHit rayhit;
                if (Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire1") && Physics.Raycast(currentCamera.Value.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out rayhit) && rayhit.collider.CompareTag("Map")) {
                    GameObject newWaypoint = ObjectPooler.instance.GetFromPool("Waypoint", rayhit.point + new Vector3(0, 1.5f, 0));
                    AddWaypoint(newWaypoint.transform);
                    SetDestination();
                }
                yield return null;
            }

            isUpdating = false;
        }
    }

    //Checks if there is a undiscovert object and discovers it
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

    //gizmo for the range of the spotting Update
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottingSphereSize);
    }

    //sets the destination for the pathfinding
    void SetDestination() {
        if (myPath.Count > 0) {
            myAgent.destination = myPath[0].position;
        }
        else {
            myAgent.destination = transform.position + (transform.forward * 150 * Time.deltaTime);
        }
    }

    //updates the waypoint path
    void UpdateWaypointPath(bool boatUpdate) {

        if (boatUpdate == true) {
            waypointLines.SetPosition(0, transform.position + new Vector3(0, 1.5f, 0));
        }
        else {
            waypointLines.SetPosition(0, transform.position + new Vector3(0, 1.5f, 0));
            for (int i = 0; i < myPath.Count; i++) {

                waypointLines.SetPosition(i + 1, myPath[i].position);
            }
        }
    }
}