using UnityEngine;
using UnityEngine.Events;

public class CollisionExitEvent : MonoBehaviour {
    [Multiline] [SerializeField] string info;
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnTriggerExit() {
        for(int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}
