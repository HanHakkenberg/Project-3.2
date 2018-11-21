using UnityEngine;
using UnityEngine.Events;

public class InitiationEvent : MonoBehaviour {
    [SerializeField] UnityEvent startUnityEvent;
    [SerializeField] GameEvent[] startEvent;

    [SerializeField] UnityEvent awakeUnityEvent;

    void Awake() {
        awakeUnityEvent.Invoke();
    }

    void Start () {
        for(int i = 0; i < startEvent.Length; i++) {
            startEvent[i].Raise();
        }
        startUnityEvent.Invoke();
	}
	
}
