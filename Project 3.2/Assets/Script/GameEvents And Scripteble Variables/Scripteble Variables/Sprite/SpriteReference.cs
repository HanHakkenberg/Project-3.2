﻿using UnityEngine;

[System.Serializable]
public class SpriteReference {
    Sprite baseValue;
    [SerializeField] Sprite value;
    [SerializeField] SpriteVariable variable;
    [SerializeField] bool useConstant;
    [SerializeField] GameEvent myEvent;

    void Awake() {
        baseValue = value;
    }

    public Sprite Value {
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