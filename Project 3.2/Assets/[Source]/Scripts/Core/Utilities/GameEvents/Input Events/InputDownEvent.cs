using UnityEngine;
using UnityEngine.Events;

public class InputDownEvent : MonoBehaviour {
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;
    [SerializeField] string myInput;

    void Update() {
        if(Input.GetButtonDown(myInput)) {
            for(int i = 0; i < myEvent.Length; i++) {
                myEvent[i].Raise();
            }

            myUnityEvent.Invoke();
        }
    }
}
