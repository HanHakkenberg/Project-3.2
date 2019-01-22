using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject
{
    [Header("Building General")]
    public new string name;
    public string description;

    [Header ("Building Stats")]
    public int foodStat; 
    public int materialStat;
    public int moneyStat;
    public int citizenStat;

    [Header("Building cost")]
    public string costDescription;
    public int materialCost;
    public int moneyCost;
    public int citizenCost;
}
