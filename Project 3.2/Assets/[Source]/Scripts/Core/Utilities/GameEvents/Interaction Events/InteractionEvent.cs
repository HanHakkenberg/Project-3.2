using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour {
    [Header("Interact")]
    [SerializeField] GameEvent interactGameEvent;
    [SerializeField] UnityEvent interactUnityEvent;

    [Header("Deinteract")]
    [SerializeField] GameEvent deinteractGameEvent;
    [SerializeField] UnityEvent deinteractUnityEvent;

    [Header("Select/Highlight")]
    [SerializeField] GameEvent selectGameEvent;
    [SerializeField] UnityEvent selectUnityEvent;

    [Header("Deselect/Unhighlight")]
    [SerializeField] GameEvent deselectGameEvent;
    [SerializeField] UnityEvent deselectUnityEvent;

    public void InteractionHighlight() {
        if(selectGameEvent != null) {
            selectGameEvent.Raise();
        }

        selectUnityEvent.Invoke();
    }

    public void InteractionUnhighlight() {
        if(deselectGameEvent != null) {
            deselectGameEvent.Raise();
        }

        deselectUnityEvent.Invoke();
    }

    public void Interact() {
        if(interactGameEvent != null) {
            interactGameEvent.Raise();
        }

        interactUnityEvent.Invoke();
    }

    public void Deinteract() {
        if(deinteractGameEvent != null) {
            deinteractGameEvent.Raise();
        }

        deinteractUnityEvent.Invoke();
    }
}
