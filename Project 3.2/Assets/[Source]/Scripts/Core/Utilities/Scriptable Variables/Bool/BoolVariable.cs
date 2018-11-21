using UnityEngine;

[CreateAssetMenu(fileName = "Bool", menuName = "Variable/Bool")]
public class BoolVariable : ScriptableObject {
    [Multiline] [SerializeField] string variableInfo;

    [SerializeField] bool baseValue;
    [SerializeField] bool value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public bool Value {
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