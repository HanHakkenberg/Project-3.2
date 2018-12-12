using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

    public bool looted;
    public CivManager.Type rDemand;
    public CivManager.Type rExcess;
    [SerializeField] TransformReference currentSelected;

    void Start() 
    {
        RandomizeIsland();
    }

    public void RandomizeIsland()
    {
        int demand = Random.Range(0,3);
        int excess = Random.Range(0,3);
        if (demand == excess)
        {
            excess = Random.Range(0,3);
        }
        
        rDemand = (CivManager.Type)demand;
        rExcess = (CivManager.Type)excess;
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;

            IslandInteractionManager.instance.IslandInsert(this);
        }
    }
}