using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Event", menuName = "Event/ScriptableEvent", order = 1)]
public class ScriptableEvent : ScriptableObject
{
    
    public List<ResourceDropdown> affectedResources = new List<ResourceDropdown>();
    public string Title;
    [Multiline]
    public string Message;
}

[System.Serializable]
public class ResourceDropdown
{
    [Tooltip("Type of resource changed by event")]
    public CivManager.Type ResoureType;
    [Tooltip("Value changed by event")]
    public int Value;
}
