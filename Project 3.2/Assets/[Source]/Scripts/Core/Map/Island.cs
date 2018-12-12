using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

    public bool looted;
    CivManager.Type rDemand;
    CivManager.Type rExcess;
    [SerializeField] TransformReference currentSelected;

    void Start() 
    {
        
    }

    public void RandomizeIsland()
    {
        // rDemand =CivManager.Type
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;

            IslandInteractionManager.instance.IslandInsert(this);
        }
    }
}