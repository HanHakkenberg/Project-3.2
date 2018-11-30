using UnityEngine;

[CreateAssetMenu(fileName = "LayerMask", menuName = "Variable/LayerMask")]
public class LayerMaskVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] LayerMask baseValue;
    [SerializeField] LayerMask value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public LayerMask Value {
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