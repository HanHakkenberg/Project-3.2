using UnityEngine;

[CreateAssetMenu(fileName = "Bool", menuName = "Variable/Bool")]
public class BoolVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] bool baseValue;
    [SerializeField] bool value;

    public bool Value {
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

    [SerializeField] GameEvent myEvent;
    [SerializeField] bool reset = true;

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