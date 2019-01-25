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
        GameManager.longGameplayTick += UpdateIsland;
    }

    void UpdateIsland() 
    {
        if (interactionState == InteractionState.Settled)
        {
            if (!looted)
            {
                //attitude code
                if (attitude > 0)
                {
                    if (attitude <= 5)
                    {
                        attitude = 0;
                    }
                    else
                    {
                        float value = Mathf.Log(attitude, 2);
                        print(value);
                        value *= -1;
                        UpdateAttitude(value);
                        if (attitude < 0)
                        {
                            attitude = 0;
                        }
                    }
                }
                else if(attitude < 0)
                {
                    if (attitude >= -5)
                    {
                        attitude = 0;
                    }
                    else
                    {
                        attitude = Mathf.Abs(attitude);
                        float value = Mathf.Log(attitude, 2);
                        print(value);
                        attitude *= -1;
                        UpdateAttitude(value);
                        if (attitude > 0)
                        {
                            attitude = 0;
                        }   
                    }
                }
                //tradeleft code
                if (amountTraded != 0)
                {
                    amountTraded -= 10;
                }
            }   
        }

        if (looted)
        {
            if (recoveryTimer != 0)
            {
                print("Test 2");
                recoveryTimer -= 1;
            }
            if (recoveryTimer == 0)
            {
                print("Test 3");
                looted = false;
                if (IslandInteractionManager.instance.activeIsland == this)
                {
                    IslandInteractionManager.instance.ToggleInteractionPannels(this);
                }
            }
        }
    }

    public override void InsertInteractionManager() {
        print("Klicked On island");
        IslandInteractionManager.instance.InteractableObjectInsert(this);
    }

    void RandomizeIsland() {
        int demand = Random.Range(0, 3);
        int excess = Random.Range(0, 3);
        while (demand == excess) {
            excess = Random.Range(0, 3);
        }

        rDemand = (CivManager.Type)demand;
        rExcess = (CivManager.Type)excess;

        UpdateAttitude(Random.Range(-25, 51));

        int interactionTypeVar = Random.Range(0, 11);
        if (interactionTypeVar > 6) {
            interactionState = InteractionState.Unsettled;
        }
        else {
            interactionState = InteractionState.Settled;
        }
    }

    /// <summary>
    /// Call this function if you want to change a Islands attitude
    /// </summary>
    /// <param name="value">The value added to the current attitude</param>
    public void UpdateAttitude(float value) {
        attitude += value;
        attitude = Mathf.Floor(attitude);
        attitude = Mathf.Clamp(attitude, -100, 100);
        int trading = Mathf.FloorToInt(baseTrading + attitude / 2);
        maxTrading = trading;
    }
}