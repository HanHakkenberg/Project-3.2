using UnityEngine;

public class InputDownEvent : MonoBehaviour {
	[SerializeField] GameEvent myEvent;
	[SerializeField] string myInput;

	void Update() {
		if (Input.GetButtonDown(myInput)) {
			myEvent.Raise();
		}
	}
}
