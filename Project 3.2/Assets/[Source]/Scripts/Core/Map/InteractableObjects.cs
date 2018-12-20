using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjects : MonoBehaviour
{
    public bool canInteract = false;
    [SerializeField] TransformReference currentSelected;
    void OnMouseDown() 
    {
        print("mouseDown");
        if (!Input.GetButton("Waypoint Interact") && Input.GetButtonDown("Fire1")) {
            currentSelected.Value = transform;
        }
    }
    public abstract void InsertInteractionManager();
}
