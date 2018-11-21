using UnityEngine;

[CreateAssetMenu(fileName = "Vector4", menuName = "Variable/Vector4")]
public class Vector4Variable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Vector4 baseValue;
    [SerializeField] Vector4 value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public Vector4 Value {
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