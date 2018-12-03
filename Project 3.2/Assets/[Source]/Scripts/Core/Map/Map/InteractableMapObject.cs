using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMapObject : MonoBehaviour {
    [SerializeField] GameObject spottedObject;
    [SerializeField] TransformReference currentSelected;

    void Start() {
        spottedObject.SetActive(false);
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;
            StartInteraction();
        }
    }

    public void StopInteraction() {

    }

    public void StartInteraction() {

    }
}