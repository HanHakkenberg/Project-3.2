using UnityEngine.Events;
using UnityEngine;

public class MouseDownEvent : MonoBehaviour
{
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnMouseDown() {
        for (int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}
