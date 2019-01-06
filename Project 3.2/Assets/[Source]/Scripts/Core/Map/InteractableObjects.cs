using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjects : MonoBehaviour
{
    public enum InteractionState
    {
        Unsettled,
        Settled,
        LootSite
    }
    public bool explored;
    public bool looted;
    public InteractionState interactionState;

    public bool canInteract = false;
    [SerializeField] TransformReference currentSelected;
    void OnMouseDown() 
    {
        print("mouseDown");
        if (!Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire1")) {
            currentSelected.Value = transform;
            InsertInteractionManager();
        }
    }
    public abstract void InsertInteractionManager();
}
