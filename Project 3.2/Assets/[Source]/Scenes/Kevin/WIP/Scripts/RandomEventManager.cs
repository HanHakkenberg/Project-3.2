using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager instance;

    public List<ScriptableEvent> randomEventsList = new List<ScriptableEvent>();

    public GameObject eventPannel;
    public ScriptableEvent activeEvent{ get; private set; }

    /// <summary>
    /// Option button prefab
    /// </summary>
    public GameObject button;
    public Transform buttonLocation;

    public TMP_Text eventText;
    public TMP_Text eventTitle;

    string previouseSpeedString;

    void Awake() 
    {
         if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        GameManager.longGameplayTick += TriggerRandomEvent;
    }

    void Start() 
    {
        ToolTipPopup.toolTipPannel = GameObject.FindGameObjectWithTag("ToolTip");
        ToolTipPopup.toolTip = ToolTipPopup.toolTipPannel.transform;  
        ToolTipPopup.toolTipText = ToolTipPopup.toolTip.GetComponentInChildren<TMP_Text>();
        ToolTipPopup.toolTipPannel.SetActive(false);
    }

    /// <summary>
    /// Call this function to have a chance of triggering a random event
    /// </summary>
    public void TriggerRandomEvent()
    {
        // Using the Chance int combined with the if statement gives the random event a chance to trigger instead of a guarantee
        int Chance = Random.Range(1,11);
        
        if (Chance > 5)
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
        //Setup pannel
        eventPannel.SetActive(true);
        eventText.text = curentEvent.Message;
        eventTitle.text = curentEvent.Title;

        //Create buttons
        for (int i = 0; i < curentEvent.eventOptions.Count; i++)
        {
            int t;
            GameObject newButton = Instantiate(button,buttonLocation.position,Quaternion.identity);
            newButton.transform.SetParent(buttonLocation);
            newButton.GetComponentInChildren<TMP_Text>().text = curentEvent.eventOptions[i].buttonText;

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
                CivManager.instance.AddIncome(effects.Value,effects.resoureType);    
            }
            else
            {
                CivManager.instance.RemoveIncome(effects.Value,effects.resoureType);
            }
        }
        eventPannel.SetActive(false);
        eventText.text = "EventText";
        eventTitle.text = "Titel";
        TimeManager.instance.PauseGameSpeed();
        activeEvent = null; 
    }
}
