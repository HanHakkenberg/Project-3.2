using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour {
    [Multiline] [SerializeField] string info;
    [SerializeField] GameEvent[] myEvent;
    [SerializeField] UnityEvent myUnityEvent;
    [SerializeField] float Delay;

    public void StartDelay(){
        StartCoroutine(Timer());
    }

    public void StopDelay() {
        StopCoroutine(Timer());
    }

    IEnumerator Timer() {
        StopCoroutine(Timer());

        yield return new WaitForSeconds(Delay);

        for(int i = 0; i < myEvent.Length; i++) {
            myEvent[i].Raise();
        }

        myUnityEvent.Invoke();
    }
}
