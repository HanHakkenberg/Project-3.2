using UnityEngine;
[CreateAssetMenu(fileName = "Vector2", menuName = "Variable/Vector2")]
public class Vector2Variable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] Vector2 baseValue;
    [SerializeField] Vector2 value;

    public Vector2 Value {
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