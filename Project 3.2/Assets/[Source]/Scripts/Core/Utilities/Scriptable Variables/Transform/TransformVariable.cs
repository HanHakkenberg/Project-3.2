using UnityEngine;

[CreateAssetMenu(fileName = "Transform", menuName = "Variable/Transform")]
public class TransformVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Transform baseValue;
    [SerializeField] Transform value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public Transform Value {
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