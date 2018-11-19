using UnityEngine;
using UnityEngine.UI;

public class SliderEvent : MonoBehaviour {
	[SerializeField] FloatReference output;
	[SerializeField] int maxValue;
	[SerializeField] Slider target;

	public void UpdateValue() {
		output.Value = maxValue * target.value;
	}
}