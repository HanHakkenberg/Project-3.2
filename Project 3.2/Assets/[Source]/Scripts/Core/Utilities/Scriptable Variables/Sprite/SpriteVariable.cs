using UnityEngine;

[CreateAssetMenu(fileName = "Sprite", menuName = "Variable/Sprite")]
public class SpriteVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Sprite baseValue;
    [SerializeField] Sprite value;

    public Sprite Value {
        get {
            return value;
        }
        set {
            this.value = value;

            if (myEvent != null) {
                myEvent.Raise();
            }
        }
    }

    [SerializeField] GameEvent myEvent;
    [SerializeField] bool reset = true;

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