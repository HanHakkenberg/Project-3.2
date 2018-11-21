using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enum", menuName = "Variable/Enum/Enum")]
public class EnumVariable : ScriptableObject {
	[Multiline][SerializeField] string variableInfo;

	public EnumState baseValue;
	[SerializeField] EnumState value;
	[SerializeField] List<EnumState> enumStates = new List<EnumState>();

	[SerializeField] GameEvent myEvent;
	[SerializeField] bool reset = true;

	public EnumState Value {
		get {
		return value;
		}
		set {
			this.value = value;

			if (myEvent != null) {
				myEvent.Raise();
			}
		}
	}

	public void OnEnable() {
		if (reset == true) {
			value = baseValue;
		}
	}

	public void ManualReset() {
		if (reset == true) {
			value = baseValue;
		}
	}
}