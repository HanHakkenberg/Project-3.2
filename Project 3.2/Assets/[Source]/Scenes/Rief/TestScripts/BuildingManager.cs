using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BuildingInfo
{
    public string name;
    public GameObject buildingModel;
    public Building myBuilding;

    public int myFood;
    public int myMats;
    public int myMoney;
    public int myCitizens;
}

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager instance;

    public int allBuildingFood;
    public int allBuildingMats;
    public int allBuildingMoney;
    public int allBuildingCitizens;

    public List<BuildingInfo> allBuildings = new List<BuildingInfo>();



    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void AddBuilding(List<BuildingInfo> _building)
    {
        BuildingManager.instance.allBuildings.Add(_building[0]);
        BuildingManager.instance.allBuildingFood += _building[0].myFood;
        BuildingManager.instance.allBuildingMats += _building[0].myMats;
        BuildingManager.instance.allBuildingMoney += _building[0].myMoney;
        BuildingManager.instance.allBuildingCitizens += _building[0].myCitizens;
    }
}
