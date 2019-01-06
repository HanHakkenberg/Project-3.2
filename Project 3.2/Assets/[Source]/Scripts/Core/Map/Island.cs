using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : InteractableObjects {

    
    public static Ship ship;
    int baseTrading = 100;
    public int maxTrading { get; private set; }
    public int amountTraded;
    public CivManager.Type rDemand;
    public CivManager.Type rExcess;
    public float attitude = 0;

    void Start() {
        RandomizeIsland();
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

        UpdateAttitude(Random.Range(-25,51));

        int interactionTypeVar = Random.Range(0,11);
        if(interactionTypeVar > 6)
        {
            interactionState = InteractionState.Unsettled;
        }
        else
        {
            interactionState = InteractionState.Settled;            
        }
    }

    /// <summary>
    /// Call this function if you want to change a Islands attitude
    /// </summary>
    /// <param name="value">The value added to the current attitude</param>
    public void UpdateAttitude(float value) {
        attitude += value;
        Mathf.Clamp(attitude, -100, 100);
        int trading = Mathf.FloorToInt(baseTrading + attitude / 2);
        maxTrading = trading;
    }
}

