using UnityEngine;

[CreateAssetMenu(fileName = "Vector3", menuName = "Variable/Vector3")]
public class Vector3Variable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Vector3 baseValue;
    [SerializeField] Vector3 value;

    public Vector3 Value {
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