using UnityEngine;

public class EventTrigger : MonoBehaviour {
	[SerializeField] GameEvent myEvent;

	public void RaiseEvent() {
		myEvent.Raise();
	}
}