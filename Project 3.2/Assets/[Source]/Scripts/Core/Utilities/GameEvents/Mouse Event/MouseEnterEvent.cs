using UnityEngine;
using UnityEngine.Events;

public class MouseEnterEvent : MonoBehaviour {
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnMouseEnter() {
        for (int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}