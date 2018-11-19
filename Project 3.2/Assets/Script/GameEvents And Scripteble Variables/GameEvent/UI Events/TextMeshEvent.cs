using TMPro;
using UnityEngine;

public class TextMeshEvent : MonoBehaviour {
	[SerializeField] TextMeshProUGUI target;

	[Header("Change Text Input")]
	[SerializeField] StringReference textInput;

	public void UpdateTextInput() {
		target.text = textInput.Value;
	}

	[Header("Change Color")]
	[SerializeField] Vector4Reference textRGBA;

	public void UpdateTextColor() {
		target.color = textRGBA.Value;
	}
}