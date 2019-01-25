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
    public int buildingNum;

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
        GameManager.shortGameplayTick += RemoveStats;
    }

    void Update()
    {
        
    }
    public void AddBuilding(List<BuildingInfo> _building)
    {
        BuildingManager.instance.allBuildings.Add(_building[0]);
        AddStats();
        buildingNum++;
    }
    public void AddStats()
    {
        allBuildingFood = 0;
        allBuildingMats = 0;
        allBuildingMoney = 0;
        allBuildingCitizens = 0;
        for (int i = 0; i < allBuildings.Count; i++)
        { 
            allBuildingFood += allBuildings[i].myFood;
            allBuildingMats += allBuildings[i].myMats;
            allBuildingMoney += allBuildings[i].myMoney;
            allBuildingCitizens += allBuildings[i].myCitizens;
        }
    }

    public void RemoveStats()
    {
        if(allBuildingFood > 0)
        {
            CivManager.instance.AddIncome(allBuildingFood, CivManager.Type.Food);
        }
        else
        {
            CivManager.instance.RemoveIncome(allBuildingFood, CivManager.Type.Food);
        }
        
        if(allBuildingMats > 0)
        {
            CivManager.instance.AddIncome(allBuildingMats, CivManager.Type.Mats);
        }
        else
        {
            CivManager.instance.RemoveIncome(allBuildingMats, CivManager.Type.Mats);
        }
        
        if(allBuildingMoney > 0)
        {
            CivManager.instance.AddIncome(allBuildingMoney, CivManager.Type.Money);
        }
        else
        {
            CivManager.instance.RemoveIncome(allBuildingMoney, CivManager.Type.Money);
        }
        
    }
}
