using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {
    public static Ship ship;
    public bool canInteract = false;

    public bool looted;
    public bool settled;

    public int maxTrading { get; private set; }
    public int amountTraded;
    public CivManager.Type rDemand;
    public CivManager.Type rExcess;
    public float attitude = 0;
    [SerializeField] TransformReference currentSelected;

    void Start() {
        RandomizeIsland();
        maxTrading = 100;
    }

    public void RandomizeIsland() {
        int demand = Random.Range(0, 3);
        int excess = Random.Range(0, 3);
        while (demand == excess) {
            excess = Random.Range(0, 3);
        }

        rDemand = (CivManager.Type)demand;
        rExcess = (CivManager.Type)excess;
    }

    /// <summary>
    /// Call this function if you want to change a Islands attitude
    /// </summary>
    /// <param name="value">The value added to the current attitude</param>
    public void UpdateAttitude(float value) {
        attitude += value;
        Mathf.Clamp(attitude, -100, 100);
    }

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;
            print("test");
            IslandInteractionManager.instance.IslandInsert(this);
        }
    }
}