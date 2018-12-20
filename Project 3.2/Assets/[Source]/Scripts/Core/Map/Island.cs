using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : InteractableObjects {

    public enum IslandState
    {
        Unexplored,
        Unsettled,
        Settled,
        looted
    }
    public IslandState islandState;
    public static Ship ship;

    public int maxTrading { get; private set; }
    public int amountTraded;
    public CivManager.Type rDemand;
    public CivManager.Type rExcess;
    public float attitude = 0;

    void Start() {
        RandomizeIsland();
        maxTrading = 100;
    }

    public override void InsertInteractionManager()
    {
        IslandInteractionManager.instance.IslandInsert(this);
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
}

