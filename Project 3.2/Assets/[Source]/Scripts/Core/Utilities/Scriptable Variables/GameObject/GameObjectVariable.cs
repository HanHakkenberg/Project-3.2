using UnityEngine;

[CreateAssetMenu(fileName = "GameObject", menuName = "Variable/GameObject")]
public class GameObjectVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] GameObject baseValue;
    [SerializeField] GameObject value;

    public GameObject Value {
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