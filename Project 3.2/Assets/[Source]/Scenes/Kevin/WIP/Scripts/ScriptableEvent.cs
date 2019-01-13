using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Event", menuName = "Event/ScriptableEvent", order = 1)]
public class ScriptableEvent : ScriptableObject
{
    public List<EventOptions> eventOptions = new List<EventOptions>();
    public string Title;
    [Multiline]
    public string Message;
}

[System.Serializable]
public class EventOptions
{
    public List<EventOptionsEffects> eventOptionsEffects = new List<EventOptionsEffects>();
    public string buttonText;
}

[System.Serializable]
public class EventOptionsEffects
{
    //This class contains all the effects a Events options performs

    [Tooltip("Type of resource changed by event")]
    public CivManager.Type ResoureType;
    [Tooltip("Value changed by event")]
    public int Value;
}