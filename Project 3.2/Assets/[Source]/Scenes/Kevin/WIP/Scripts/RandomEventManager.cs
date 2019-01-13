using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEventManager : MonoBehaviour
{
    public List<ScriptableEvent> randomEventsList = new List<ScriptableEvent>();
    public List<ScriptableEvent> EventsList = new List<ScriptableEvent>();
    ScriptableEvent activeEvent;
    public GameObject button;
    public Transform buttonLocation;
    public TMP_Text eventText;
    public TMP_Text eventTitle;


    /// <summary>
    /// Call this function to have a chance of triggering a random event
    /// </summary>
    public void TriggerRandomEvent()
    {
        // Using the Chance int combined with the if statement gives the random event a chance to trigger instead of a guarantee
        int Chance = Random.Range(1,11);
        if (Chance > 8)
        {
            int eventIndex = Random.Range(0,randomEventsList.Count);
            activeEvent = randomEventsList[eventIndex];
            PlayEvent(activeEvent);
        }
    }

    /// <summary>
    /// Call this function whit the event you want to activate
    /// </summary>
    /// <param name="curentEvent">The event you want to activate</param>
    public void PlayEvent(ScriptableEvent curentEvent)
    {
        eventText.text = curentEvent.Message;
        eventTitle.text = curentEvent.Title;
        for (int i = 0; i < curentEvent.eventOptions.Count; i++)
        {
            Instantiate(button,buttonLocation.position,Quaternion.identity);
        }
    }
}
