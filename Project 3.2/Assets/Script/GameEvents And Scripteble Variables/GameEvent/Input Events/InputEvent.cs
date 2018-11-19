using UnityEngine;

public class InputEvent : MonoBehaviour {
	[SerializeField] GameEvent myEvent;
	[SerializeField] string myInput;

	void Update() {
		if (Input.GetButton(myInput)) {
			myEvent.Raise();
		}
	}
}