using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Variable/Float")]
public class FloatVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] float baseValue;
    [SerializeField] float value;

    public float Value {
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