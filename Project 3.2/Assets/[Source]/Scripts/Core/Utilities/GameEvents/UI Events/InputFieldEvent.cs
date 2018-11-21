using UnityEngine;
using UnityEngine.UI;

public class InputFieldEvent : MonoBehaviour {
	[SerializeField] StringReference output;
	[SerializeField] InputField target;

	public void UpdateValue() {
		output.Value = target.text;
	}
}
