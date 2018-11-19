using UnityEngine;

public class InputUpEvent : MonoBehaviour {
	[SerializeField] GameEvent myEvent;
	[SerializeField] string myInput;

	void Update() {
		if (Input.GetButtonUp(myInput)) {
			myEvent.Raise();
		}
	}
}