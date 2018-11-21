using UnityEngine;
using UnityEngine.Events;

public class CollisionStayEvent : MonoBehaviour {
    [Multiline] [SerializeField] string info;
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnTriggerStay() {
        for(int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}
