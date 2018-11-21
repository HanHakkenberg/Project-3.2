using UnityEngine;

[CreateAssetMenu(fileName = "Vector4", menuName = "Variable/Vector4")]
public class Vector4Variable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Vector4 baseValue;
    [SerializeField] Vector4 value;

    public Vector4 Value {
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