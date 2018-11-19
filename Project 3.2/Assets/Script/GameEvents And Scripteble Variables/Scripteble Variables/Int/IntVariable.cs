using UnityEngine;

[CreateAssetMenu(fileName = "Int", menuName = "Variable/Int")]
public class IntVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] int baseValue;
    [SerializeField] int value;

    public int Value {
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