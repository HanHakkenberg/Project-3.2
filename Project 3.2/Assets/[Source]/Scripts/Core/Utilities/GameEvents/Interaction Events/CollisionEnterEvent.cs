using UnityEngine;
using UnityEngine.Events;

public class CollisionEnterEvent : MonoBehaviour {
    [SerializeField] [Multiline] string comment;
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnCollisionEnter() {
        for(int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}
