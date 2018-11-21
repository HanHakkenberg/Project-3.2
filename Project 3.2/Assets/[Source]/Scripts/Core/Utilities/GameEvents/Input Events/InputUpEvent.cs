using UnityEngine;
using UnityEngine.Events;

public class InputUpEvent : MonoBehaviour {
	[SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;
    [SerializeField] string myInput;

	void Update() {
		if (Input.GetButtonUp(myInput)) {
            if(myEvent != null) {
                for(int i = 0; i < myEvent.Length; i++) {
                    myEvent[i].Raise();
                }
            }
            myUnityEvent.Invoke();
		}
	}
}