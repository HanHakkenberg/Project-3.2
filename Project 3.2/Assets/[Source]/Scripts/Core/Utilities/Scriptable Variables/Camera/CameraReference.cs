using UnityEngine;

[System.Serializable]
public class CameraReference {
    Camera baseValue;
    [SerializeField] Camera value;
    [SerializeField] CameraVariable variable;
    [SerializeField] bool useConstant;
    [SerializeField] GameEvent myEvent;

    void Awake() {
        baseValue = value;
    }

    public Camera Value {
        get {
            if (useConstant) {
                return value;
            } else {
                return variable.Value;
            }
        }
        set {
            if (useConstant) {
                this.value = value;
            } else {
                this.variable.Value = value;
            }

            if (myEvent != null) {
                myEvent.Raise();
            }
        }
    }

    public void ManualReset() {
        if (variable != null) {
            variable.ManualReset();
        } else {
            value = baseValue;
        }
    }
}