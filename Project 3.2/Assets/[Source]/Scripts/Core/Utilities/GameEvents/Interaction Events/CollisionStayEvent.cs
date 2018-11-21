using UnityEngine;
using UnityEngine.Events;

public class CollisionStayEvent : MonoBehaviour {
    [SerializeField] GameEvent myGameEvent;
    [SerializeField] UnityEvent myEvent;

    void OnTriggerStay() {
        if(myGameEvent != null) {
            myGameEvent.Raise();
        }

        myEvent.Invoke();
    }
}
