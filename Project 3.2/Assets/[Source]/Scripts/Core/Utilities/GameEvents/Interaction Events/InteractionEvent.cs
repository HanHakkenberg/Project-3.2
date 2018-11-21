using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour {
    [Header("Interact")]
    [SerializeField] GameEvent[] interactGameEvent;
    [SerializeField] UnityEvent interactUnityEvent;

    [Header("Deinteract")]
    [SerializeField] GameEvent[] deinteractGameEvent;
    [SerializeField] UnityEvent deinteractUnityEvent;

    [Header("Select/Highlight")]
    [SerializeField] GameEvent[] selectGameEvent;
    [SerializeField] UnityEvent selectUnityEvent;

    [Header("Deselect/Unhighlight")]
    [SerializeField] GameEvent[] deselectGameEvent;
    [SerializeField] UnityEvent deselectUnityEvent;

    public void InteractionHighlight() {
        for(int i = 0; i < selectGameEvent.Length; i++) {
            selectGameEvent[i].Raise();
        }

        selectUnityEvent.Invoke();
    }

    public void InteractionUnhighlight() {
        for(int i = 0; i < deselectGameEvent.Length; i++) {
            deselectGameEvent[i].Raise();
        }

        deselectUnityEvent.Invoke();
    }

    public void Interact() {
        for(int i = 0; i < interactGameEvent.Length; i++) {
            interactGameEvent[i].Raise();
        }

        interactUnityEvent.Invoke();
    }

    public void Deinteract() {
        for(int i = 0; i < deinteractGameEvent.Length; i++) {
            deinteractGameEvent[i].Raise();
        }

        deinteractUnityEvent.Invoke();
    }
}
