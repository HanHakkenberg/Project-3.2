using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSite : InteractableObjects
{
    public CivManager.Type lootType;
    public GameObject parent;

    void Start()
    {
        RandomizeLootSite();
    }

    void Update() 
    {
        KillSelf();
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

    public void Sink()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("Sink");
    }

    public void KillSelf()
    {
        if (transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("End"))
        {
            Destroy(parent);
            print("ded");
        }
    }
}
