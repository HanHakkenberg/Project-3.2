using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Unlock", menuName = "Island Unlockable")]
public class IslandLock : ScriptableObject
{
    public new string name;
    public string description;

    public int materialCost;
    public int moneyCost;
    public int citizenCost;
}
