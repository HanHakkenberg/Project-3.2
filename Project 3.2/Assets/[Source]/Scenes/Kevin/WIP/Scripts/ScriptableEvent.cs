using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Event", menuName = "Event/ScriptableEvent", order = 1)]
public class ScriptableEvent : ScriptableObject
{
    public List<EventOptions> eventOptions = new List<EventOptions>();
    [Tooltip("Event titel text")]
    public string Title;
    [Tooltip("Event message text")]
    [Multiline]
    public string Message;
}

[System.Serializable]
public class EventOptions
{
    [Tooltip("Text which will be shown on the button")]
    public string buttonText;
    public List<EventOptionsEffects> eventOptionsEffects = new List<EventOptionsEffects>();
}

[System.Serializable]
public class EventOptionsEffects
{
    //This class contains all the effects a Events options performs

    [Tooltip("Type of resource changed by event")]
    public CivManager.Type resoureType;
    [Tooltip("Value changed by event")]
    public int Value;
}