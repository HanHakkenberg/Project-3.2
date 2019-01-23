using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChain", menuName = "Event/EventChain", order = 1)]
public class EventChain : ScriptableObject
{
    public List<ChainPart> chainParts = new List<ChainPart>();
}

[System.Serializable]
public class ChainPart
{
    public ScriptableEvent eventPart;
    public int ticksTillTrigger;
}
