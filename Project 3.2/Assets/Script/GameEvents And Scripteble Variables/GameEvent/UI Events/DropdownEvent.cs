using UnityEngine;
using UnityEngine.UI;

public class DropdownEvent : MonoBehaviour {
	[SerializeField] IntReference output;
	[SerializeField] Dropdown target;

	public void UpdateValue() {
		output.Value = target.value;
	}
}
