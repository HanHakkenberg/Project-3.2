using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectListReference {
    List<float> baseValue;
    [SerializeField] List<float> value = new List<float>();
    [SerializeField] FloatListVariable variable;
    [SerializeField] bool useConstant;
    [SerializeField] GameEvent myEvent;

    public List<float> Value {
        get {
            if(useConstant) {
                return value;
            }
            else {
                return variable.Value;
            }
        }
        set {
            if(useConstant) {
                this.value = value;
            }
            else {
                this.variable.Value = value;
            }

            if(myEvent != null) {
                myEvent.Raise();
            }
        }
    }

    public void ManualReset() {
        if(variable != null) {
            variable.ManualReset();
        }
        else {
            value = baseValue;
        }
    }
}