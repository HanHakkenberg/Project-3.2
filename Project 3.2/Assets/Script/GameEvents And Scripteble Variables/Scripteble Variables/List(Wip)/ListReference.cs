using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListReference<T> : ListVariable<T>{
	[SerializeField] List<T> myValue = new List<T>();
	[SerializeField] ListVariable<T> variable;
	[SerializeField] bool useConstant;
	[SerializeField] GameEvent myEvent;

    void Awake() {
        if (variable.reset == true) {
            variable.baseValue = variable.value;
        }
    }

    public List<T> Value {
        get {
            if (useConstant) {
                return myValue;
            } else {
                return variable.value;
            }
        }
        set {
            if (useConstant) {
                this.myValue = value;
            } else {
                this.variable.value = value;
                if (myEvent != null) {
                    myEvent.Raise();
                }
            }
        }
    }
}