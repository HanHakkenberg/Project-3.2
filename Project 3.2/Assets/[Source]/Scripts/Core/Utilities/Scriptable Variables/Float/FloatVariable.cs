using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Variable/Float")]
public class FloatVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] float baseValue;
    [SerializeField] float value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public float Value {
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