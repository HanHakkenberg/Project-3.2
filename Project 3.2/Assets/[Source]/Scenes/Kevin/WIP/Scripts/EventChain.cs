using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain : MonoBehaviour
{
    public List<ChainPart> chainParts = new List<ChainPart>();
}

[System.Serializable]
public class ChainPart
{
    public ScriptableEvent eventPart;
    public int ticksTillTrigger;
}
