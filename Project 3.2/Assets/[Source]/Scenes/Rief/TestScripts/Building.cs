using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject
{
    public new string name;
    public string description;

    public int buildingNumb;

    public GameObject buildingModel;
    public int foodStat; 
    public int materialStat;
    public int moneyStat;
    public int citizenStat;
}
