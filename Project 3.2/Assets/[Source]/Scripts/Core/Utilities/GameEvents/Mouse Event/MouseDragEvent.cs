using UnityEngine;
using UnityEngine.Events;

public class MouseDragEvent : MonoBehaviour {
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnMouseDrag() {
        for (int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}