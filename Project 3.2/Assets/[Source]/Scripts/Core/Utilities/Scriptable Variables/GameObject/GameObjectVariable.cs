using UnityEngine;

[CreateAssetMenu(fileName = "GameObject", menuName = "Variable/GameObject")]
public class GameObjectVariable : ScriptableObject {
    [Multiline][SerializeField] string variableInfo;

    [SerializeField] GameObject baseValue;
    [SerializeField] GameObject value;

    [SerializeField] GameEvent[] myEvent;
    [SerializeField] bool reset = true;

    public GameObject Value {
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