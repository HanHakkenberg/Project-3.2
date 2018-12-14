using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

    public bool looted;
    public bool settled;

    public int maxTrading;
    public int amountTraded;
    public CivManager.Type rDemand;
    public CivManager.Type rExcess;
    public float attitude = 0;
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

    /// <summary>
    /// Call this function if you want to change a Islands attitude
    /// </summary>
    /// <param name="value">The value added to the current attitude</param>
    public void UpdateAttitude(float value)
    {
        attitude += value;
        Mathf.Clamp(attitude,-1,1);
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;

            IslandInteractionManager.instance.IslandInsert(this);
        }
    }
}