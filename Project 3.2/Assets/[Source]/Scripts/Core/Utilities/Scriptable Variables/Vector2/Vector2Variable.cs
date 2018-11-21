using UnityEngine;
[CreateAssetMenu(fileName = "Vector2", menuName = "Variable/Vector2")]
public class Vector2Variable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Vector2 baseValue;
    [SerializeField] Vector2 value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public Vector2 Value {
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