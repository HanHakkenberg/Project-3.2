using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseClickEvent : MonoBehaviour {
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;

    void OnMouseDown() {
        myUnityEvent.Invoke();

        for (int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }
    }

}