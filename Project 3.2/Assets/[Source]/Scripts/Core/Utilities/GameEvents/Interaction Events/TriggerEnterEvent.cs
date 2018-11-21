using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour {
    [SerializeField] GameEvent myGameEvent;
    [SerializeField] UnityEvent myEvent;

    void OnTriggerEnter() {
        if(myGameEvent != null) {
            myGameEvent.Raise();
        }

        myEvent.Invoke();
    }
}
