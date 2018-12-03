using UnityEngine;

[CreateAssetMenu(fileName = "Camera", menuName = "Variable/Camera")]
public class CameraVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Camera baseValue;
    [SerializeField] Camera value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public Camera Value {
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