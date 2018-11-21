﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject {
    [Multiline] [SerializeField] string info;

    public List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            if(listeners[i] != null) {
                listeners[i].OnEventRaise();
            }
            else {
                listeners.RemoveAt(i);
            }
        }
    }

    void OnEnable() {
        listeners = new List<GameEventListener>();
    }

    public void AddListener(GameEventListener listener) {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener) {
        listeners.Remove(listener);
    }
}
