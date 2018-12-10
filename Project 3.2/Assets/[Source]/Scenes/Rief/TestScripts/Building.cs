using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject
{
    public new string name;
    public string description;

    public int buildingNumb;

    public int tempStatOne; ///change once decided
    public int tempStatTwo;
    public int tempStatThree;
}
