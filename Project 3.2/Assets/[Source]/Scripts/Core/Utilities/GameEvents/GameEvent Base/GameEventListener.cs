using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
    [Multiline] [SerializeField] string info;

    bool isListening = false;
    [SerializeField] bool listenOnEnable = true;
    [SerializeField] bool listenOnDisable = false;

    public GameEvent[] myGameEvent;
    public UnityEvent response;

    public void StartListening() {
        if(isListening == false) {
            isListening = true;

            for(int i = 0; i < myGameEvent.Length; i++) {
                myGameEvent[i].AddListener(this);
            }
        }
    }

    public void StopListening() {
        if(isListening == true) {
            isListening = false;

            for(int i = 0; i < myGameEvent.Length; i++) {
                myGameEvent[i].RemoveListener(this);
            }
        }
    }

    public void OnEventRaise() {
        response.Invoke();
    }

    void OnEnable() {
        if(listenOnEnable) {
            StartListening();
        }
        else {
            StopListening();
        }
    }

    void OnDisable() {
        if(listenOnDisable) {
            StartListening();
        }
        else {
            StopListening();
        }
    }

    void OnDestroy() {
        StopListening();
    }
}