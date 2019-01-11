using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableEvent", order = 1)]
public class ScriptableEvent : ScriptableObject
{
    
    public List<ResourceDropdown> affedctedResources = new List<ResourceDropdown>();
    public TMP_Text Title;
    public TMP_Text Message;
}

public class ResourceDropdown
{
    CivManager.Type ResoureType;
    int Value;
}
