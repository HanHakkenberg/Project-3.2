using UnityEngine;

[System.Serializable]
public class Vector3Reference {
    Vector3 baseValue;
    [SerializeField] Vector3 value;
    [SerializeField] Vector3Variable variable;
    [SerializeField] bool useConstant;
    [SerializeField] GameEvent myEvent;

    void Awake() {
        baseValue = value;
    }

    public Vector3 Value {
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