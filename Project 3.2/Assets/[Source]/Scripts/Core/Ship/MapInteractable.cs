using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractable : MonoBehaviour{
    [SerializeField] TransformReference currentSelected;

    void OnMouseDown() {
        if(!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = null;
        }
    }

}
