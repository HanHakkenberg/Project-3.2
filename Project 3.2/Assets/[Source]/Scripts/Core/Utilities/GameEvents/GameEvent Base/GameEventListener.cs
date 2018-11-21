using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
    public GameEvent myGameEvent;
    [SerializeField] bool listenOnEnable = true;
    [SerializeField] bool listenOnDisable = false;
    public UnityEvent response;

    public void StartListening() {
        myGameEvent.AddListener(this);
    }

    public void StopListening() {
        myGameEvent.RemoveListener(this);
    }

    public void OnEventRaise() {
        response.Invoke();
    }

    void OnEnable() {
        if (listenOnEnable) {
            StartListening();
        } else {
            StopListening();
        }
    }

    void OnDisable() {
        if (listenOnDisable) {
            StartListening();
        } else {
            StopListening();
        }
    }

    void OnDestroy() {
        StopListening();
    }
}