using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomEventManager : MonoBehaviour
{
    public List<ScriptableEvent> randomEventsList = new List<ScriptableEvent>();
    public List<ScriptableEvent> EventsList = new List<ScriptableEvent>();
    public GameObject eventPannel;
    ScriptableEvent activeEvent;
    public GameObject button;
    public Transform buttonLocation;
    public TMP_Text eventText;
    public TMP_Text eventTitle;

    string previouseSpeedString;


    private void Start() {
        TriggerRandomEvent();
    }

    /// <summary>
    /// Call this function to have a chance of triggering a random event
    /// </summary>
    public void TriggerRandomEvent()
    {
        // Using the Chance int combined with the if statement gives the random event a chance to trigger instead of a guarantee
        int Chance = Random.Range(1,11);
        if (Chance > 0)
        {
            int eventIndex = Random.Range(0,randomEventsList.Count);
            activeEvent = randomEventsList[eventIndex];
            SetEvent(activeEvent);
        }
    }

    /// <summary>
    /// Call this function whit the event you want to activate
    /// </summary>
    /// <param name="curentEvent">The event you want to activate</param>
    public void SetEvent(ScriptableEvent curentEvent)
    {
        eventPannel.SetActive(true);
        eventText.text = curentEvent.Message;
        eventTitle.text = curentEvent.Title;
        for (int i = 0; i < curentEvent.eventOptions.Count; i++)
        {
            int t;
            GameObject newButton = Instantiate(button,buttonLocation.position,Quaternion.identity);
            newButton.transform.SetParent(buttonLocation);
            newButton.GetComponentInChildren<TMP_Text>().text = curentEvent.eventOptions[i].buttonText;
            t = i;
            newButton.GetComponent<Button>().onClick.AddListener(() => {ResolveEvent(t,curentEvent);});
        }
        TimeManager.instance.PauseGameSpeed();
    }

    public void ResolveEvent(int optionInt, ScriptableEvent curentEvent) 
    {
        EventOptions option = curentEvent.eventOptions[optionInt];
        
        for (int i = 0; i < option.eventOptionsEffects.Count; i++)
        {
            EventOptionsEffects effects = option.eventOptionsEffects[i];
            if (effects.Value > 0)
            {
                CivManager.instance.AddIncome(effects.Value,effects.ResoureType);    
            }
            else
            {
                CivManager.instance.RemoveIncome(effects.Value,effects.ResoureType);
            }
        }
        TimeManager.instance.PauseGameSpeed();      
    }
}
