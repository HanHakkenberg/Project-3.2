using UnityEngine;

public class EventTrigger : MonoBehaviour {
	[SerializeField] GameEvent[] myEvent;

	public void RaiseEvent() {
        for(int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }
    }
}