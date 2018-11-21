using UnityEngine;
using UnityEngine.Events;

public class CollisionEnterEvent : MonoBehaviour {
    [SerializeField] GameEvent myGameEvent;
    [SerializeField] UnityEvent myEvent;

    void OnCollisionEnter(){
        if(myGameEvent != null) {
            myGameEvent.Raise();
        }

        myEvent.Invoke();
    }
}
