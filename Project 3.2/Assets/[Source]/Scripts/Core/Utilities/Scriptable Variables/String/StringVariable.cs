using UnityEngine;

[CreateAssetMenu(fileName = "String", menuName = "Variable/String")]
public class StringVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] string baseValue;
    [SerializeField] string value;

    public string Value {
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