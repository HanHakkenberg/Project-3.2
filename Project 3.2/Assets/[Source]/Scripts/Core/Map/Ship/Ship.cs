﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour {
    [SerializeField] GameObject arrow;
    [SerializeField] GameEvent disableArrows;
    [SerializeField] TransformReference currentSelected;
    [SerializeField] TransformReference currentCamera;

    [Header("Path")]
    bool stopIt = false;
    [SerializeField] int lineWith;
    [HideInInspector] public List<Transform> myPath = new List<Transform>();
    [SerializeField] float pathSide = 5;

    [Header("Spotting")]
    [SerializeField] int SpottingRefreshTimer;
    [SerializeField] int spottingSphereSize;
    [SerializeField] int interactSphereSize;
    [SerializeField] LayerMask spottingMask;
    [SerializeField] LayerMask interactionMask;
    bool isUpdating;

    NavMeshAgent myAgent;
    LineRenderer waypointLines;

    void Awake() {
        waypointLines = GetComponent<LineRenderer>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        arrow.SetActive(false);
        waypointLines.startWidth = lineWith;
        waypointLines.endWidth = lineWith;
        StartCoroutine(IslandCheck());
        StartCoroutine(IslandInteractionCheck());
        ShipSwitch.instance.AddShip(transform);
    }

    void Update() {
        if (myPath.Count > 0) {
            if (Vector3.Distance(transform.position, myPath[0].position) < 4) {
                myPath[0].gameObject.SetActive(false);
            }
            else {
                UpdateWaypointPath();
            }
        }
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            disableArrows.Raise();
            arrow.SetActive(true);
            currentSelected.Value = transform;
            StartCoroutine(PathUpdate());
            Island.ship = this;
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

        SetDestination();
    }

    /// <summary>
    /// Adding a waypoint to the path of the ship.
    /// </summary>
    /// <param name="toAdd">Transform to add</param>
    public void AddWaypoint(Transform toAdd) {
        myPath.Add(toAdd);
        waypointLines.positionCount = myPath.Count + 1;
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
    public void UpdateWaypoint() {}

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

        arrow.SetActive(false);
    }

    //Checks if there is a undiscovert object and discovers it
    IEnumerator IslandCheck() {
        while (true) {
            Collider[] spottedObjects = Physics.OverlapSphere(transform.position, spottingSphereSize, spottingMask);

            if (spottedObjects.Length > 0) {
                ShipSwitch.instance.SopttedObject(transform);

                for (int i = 0; i < spottedObjects.Length; i++) {
                    spottedObjects[i].transform.parent.GetChild(0).gameObject.SetActive(true);
                }
            }
            yield return new WaitForSeconds(SpottingRefreshTimer);
        }
    }

    List<InteractableObjects> interactObjects = new List<InteractableObjects>();

    IEnumerator IslandInteractionCheck() {
        while (true) {
            Collider[] spottedObjects = Physics.OverlapSphere(transform.position, interactSphereSize, interactionMask);

            for (int i = 0; i < interactObjects.Count; i++) {
                if (interactObjects[i] != null) {
                    interactObjects[i].canInteract = false;
                }
            }
            interactObjects.Clear();

            if (spottedObjects.Length > 0) {

                for (int i = 0; i < spottedObjects.Length; i++) {
                    InteractableObjects l = spottedObjects[i].GetComponent<InteractableObjects>();
                    ShipSwitch.instance.Interactable(transform, true);

                    l.canInteract = true;
                    interactObjects.Add(l);
                    Island.ship = this;
                }
            }
            else {
                ShipSwitch.instance.Interactable(transform, false);
            }

            yield return null;
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
    void UpdateWaypointPath() {
        int distance = Mathf.RoundToInt(Vector3.Distance(transform.position, myPath[0].position) / pathSide);
        waypointLines.positionCount = distance + 1;
        waypointLines.SetPosition(0, transform.position + new Vector3(0, 1.5f, 0));

        for (int i = 0; i < distance; i++) {
            waypointLines.SetPosition(i + 1, Vector3.Lerp(transform.position, myPath[0].position, 1f / distance * i) + new Vector3(0, 3, 0));
        }

        if (distance == 0) {
            waypointLines.positionCount += 1;
            waypointLines.SetPosition(1, myPath[0].position + new Vector3(0, 1.5f, 0));
        }
        else {
            waypointLines.SetPosition(distance, myPath[0].position + new Vector3(0, 1.5f, 0));
        }

        if (myPath.Count > 1) {
            for (int i = 0; i < myPath.Count; i++) {
                if (myPath.Count > i + 1) {
                    distance = Mathf.RoundToInt(Vector3.Distance(myPath[i].position, myPath[i + 1].position) / pathSide);

                    for (int k = 0; k < distance; k++) {
                        waypointLines.positionCount++;

                        waypointLines.SetPosition(waypointLines.positionCount - 1, Vector3.Lerp(myPath[i].position, myPath[i + 1].position, 1f / distance * k) + new Vector3(0, 1.5f, 0));
                    }

                }
            }

            waypointLines.SetPosition(waypointLines.positionCount - 1, myPath[myPath.Count - 1].position);
        }
    }

    public void SetDestinationn(Transform myTrans) {
        myAgent.SetDestination(myTrans.position);
    }
}