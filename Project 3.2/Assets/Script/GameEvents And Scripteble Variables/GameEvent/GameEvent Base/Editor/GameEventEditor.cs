using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
[CanEditMultipleObjects]
public class GameEventEditor : Editor {
    GameEvent myEdit;

    void Awake() {
        myEdit = (GameEvent)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Raise")) {
            myEdit.Raise();
        }

        if (GUILayout.Button("Select Listeners")) {
            GameObject[] toSelect = new GameObject[myEdit.listeners.Count];
            for (int i = 0; i < myEdit.listeners.Count; i++) {
                toSelect[i] = myEdit.listeners[i].gameObject;
            }

            Selection.objects = toSelect;
        }
    }
}