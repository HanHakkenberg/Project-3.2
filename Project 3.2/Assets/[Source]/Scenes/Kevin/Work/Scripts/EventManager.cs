﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public List<EventChain> eventChains = new List<EventChain>();
    public List<ScriptableEvent> randomEventsList = new List<ScriptableEvent>();
    public ScriptableEvent activeEvent{ get; private set; }
    public EventChain activeChain{ get; private set; }
    int ticksTillEventChain;
    int chainEvent;


    /// <summary>
    /// Option button prefab
    /// </summary>
    public GameObject button;
    public GameObject eventPannel;

    public Transform buttonLocation;

    public TMP_Text eventText;
    public TMP_Text eventTitle;

    string previouseSpeedString;

    void Awake() 
    {
         if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        GameManager.longGameplayTick += TriggerEventRelatedFunctions;
    }

    void Start() 
    {
        ToolTipPopup.toolTipPannel = GameObject.FindGameObjectWithTag("ToolTip");
        ToolTipPopup.toolTip = ToolTipPopup.toolTipPannel.transform;  
        ToolTipPopup.toolTipText = ToolTipPopup.toolTip.GetComponentInChildren<TMP_Text>();
        ToolTipPopup.toolTipPannel.SetActive(false);
        StartEventchain(0);
    }

    void StartEventchain(int index)
    {
        if(activeChain == null)
        {
            activeChain = eventChains[index];
            ProgressEventChain();
        }
    }
    
    public void TriggerEventRelatedFunctions()
    {
        ProgressEventChain();
        TriggerRandomEvent();
    }

    void ProgressEventChain()
    {
        if (activeChain != null)
        {
            if (ticksTillEventChain != 0)
            {
                ticksTillEventChain -= 1;
            }
            else
            {            
                SetEvent(activeChain.chainParts[chainEvent].eventPart);
                if (chainEvent != activeChain.chainParts.Count - 1)
                {
                    chainEvent += 1;
                    ticksTillEventChain = activeChain.chainParts[chainEvent].ticksTillTrigger;
                }
                else
                {
                    activeChain = null;
                }
            }
        }
    }

    /// <summary>
    /// Call this function to have a chance of triggering a random event
    /// </summary>
    public void TriggerRandomEvent()
    {
        if (activeEvent == null)
        {
            // Using the Chance int combined with the if statement gives the random event a chance to trigger instead of a guarantee
            int Chance = Random.Range(1,11);
            
            if (Chance > 5)
            {
                int eventIndex = Random.Range(0,randomEventsList.Count);
                SetEvent(randomEventsList[eventIndex]);
            }
        }
    }

    /// <summary>
    /// Call this function whit the event you want to activate
    /// </summary>
    /// <param name="curentEvent">The event you want to activate</param>
    public void SetEvent(ScriptableEvent curentEvent)
    {
        if (activeEvent == null)
        {
            TimeManager.instance.PauseGameSpeed();
            activeEvent = curentEvent;
            //Setup pannel
            eventPannel.SetActive(true);
            eventText.text = curentEvent.Message;
            eventTitle.text = curentEvent.Title;

            //Create buttons
            for (int i = 0; i < curentEvent.eventOptions.Count; i++)
            {
                int t;
                GameObject newButton = Instantiate(button, buttonLocation.localPosition, Quaternion.identity, parent: buttonLocation);
                //newButton.transform.SetParent(buttonLocation);
                // newButton.transform.localScale = Vector3.one;
                newButton.GetComponentInChildren<TMP_Text>().text = curentEvent.eventOptions[i].buttonText;

                if(curentEvent.eventOptions.Count != 0)
                {
                    List<string> effectText = new List<string>();
                    foreach (EventOptionsEffects effect in curentEvent.eventOptions[i].eventOptionsEffects)
                    {
                        effectText.Add(effect.resoureType.ToString() + " " + effect.Value.ToString() + ". \n");
                    }
                    string formatedText = "";
                    foreach (string text in effectText)
                    {
                        formatedText += text;
                    }
                    newButton.GetComponentInChildren<ToolTipPopup>().toolTipString = formatedText;
                }
                if(curentEvent.eventOptions[i].specialEvent != 0)
                {
                    newButton.GetComponentInChildren<ToolTipPopup>().toolTipString = "This will activate a special event";
                }
                t = i;

                newButton.GetComponent<Button>().onClick.AddListener(() => {ResolveEvent(t,curentEvent);});
            }
        }
    }

    public void ResolveEvent(int optionInt, ScriptableEvent curentEvent) 
    {
        EventOptions option = curentEvent.eventOptions[optionInt];
        
        for (int i = 0; i < option.eventOptionsEffects.Count; i++)
        {
            EventOptionsEffects effects = option.eventOptionsEffects[i];
            if (effects.Value > 0)
            {
                CivManager.instance.AddIncome(effects.Value,effects.resoureType);    
            }
            else
            {
                CivManager.instance.RemoveIncome(effects.Value,effects.resoureType);
            }
        }

        if (option.specialEvent != 0)
        {
            SpecialEvents.SpecialEventsEffects(option.specialEvent);
        }
        eventPannel.SetActive(false);
        eventText.text = "EventText";
        eventTitle.text = "Titel";
        activeEvent = null;
        TimeManager.instance.PauseGameSpeed();
        WipeButtons();
    }

    void WipeButtons()
    {
        foreach (Transform child in buttonLocation)
        {
            Destroy(child.gameObject);
        }
    }
}
