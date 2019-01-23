using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjects : MonoBehaviour {
    public enum InteractionState {
        Unsettled,
        Settled,
        LootSite
    }
    public bool explored;
    public bool looted;
    public int recoveryTimer;

    public InteractionState interactionState;
    [SerializeField] GameObject arrow;

    public bool canInteract = false;
    [SerializeField] TransformReference currentSelected;
    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire1")) {
            currentSelected.Value = transform;
            InsertInteractionManager();
            arrow.SetActive(true);
        }
    }
    public abstract void InsertInteractionManager();
}