using UnityEngine;

[CreateAssetMenu(fileName = "Sprite", menuName = "Variable/Sprite")]
public class SpriteVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Sprite baseValue;
    [SerializeField] Sprite value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public Sprite Value {
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