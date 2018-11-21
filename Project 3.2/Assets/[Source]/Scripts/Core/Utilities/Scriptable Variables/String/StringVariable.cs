using UnityEngine;

[CreateAssetMenu(fileName = "String", menuName = "Variable/String")]
public class StringVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] string baseValue;
    [SerializeField] string value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public string Value {
        get {
            return value;
        }
        set {
            this.value = value;

            if (myEvent != null) {
                for(int i = 0; i < myEvent.Length; i++) {
                    myEvent[i].Raise();
                }
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