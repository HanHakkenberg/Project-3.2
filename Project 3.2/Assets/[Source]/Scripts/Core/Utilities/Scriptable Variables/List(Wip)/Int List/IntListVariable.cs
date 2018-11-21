using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int List", menuName = "Variable/List/Int List")]
public class IntListVariable : ScriptableObject {
	[Multiline][SerializeField] string variableInfo;

	[SerializeField] List<float> value = new List<float>();
	[SerializeField] List<float> baseValue = new List<float>();

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public List<float> Value {
        get {
            return value;
        }
        set {
            this.value = value;

            if(myEvent != null) {
                for(int i = 0; i < myEvent.Length; i++) {
                    myEvent[i].Raise();
                }
            }
        }
    }

    public void OnEnable() {
        if(reset == true) {
            value = baseValue;
        }
    }

    public void ManualReset() {
        if(reset == true) {
            value = baseValue;
        }
    }
}
