using UnityEngine;
using UnityEngine.UI;

public class ToggleEvent : MonoBehaviour {
	[SerializeField] BoolReference output;
	[SerializeField] Toggle myToggle;

	public void UpdateValue() {
		output.Value = myToggle.isOn;
	}
}