using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSite : InteractableObjects
{
    public CivManager.Type lootType;


    void Start()
    {
        RandomizeLootSite();
    }

    void RandomizeLootSite()
    {
        int type = Random.Range(0, 4);
        lootType = (CivManager.Type)type;
        interactionState = InteractionState.LootSite;
    }

    public override void InsertInteractionManager()
    {
        IslandInteractionManager.instance.InteractableObjectInsert(this);
    }
}
