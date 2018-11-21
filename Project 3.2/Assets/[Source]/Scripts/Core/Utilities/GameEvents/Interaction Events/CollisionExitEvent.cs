using UnityEngine;
using UnityEngine.Events;

public class CollisionExitEvent : MonoBehaviour {
    [SerializeField] GameEvent myGameEvent;
    [SerializeField] UnityEvent myEvent;

    void OnTriggerExit() {
        if(myGameEvent != null) {
            myGameEvent.Raise();
        }

        myEvent.Invoke();
    }
}
