using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelection : MonoBehaviour
{
    public IslandLock thisLock;
    public GameObject infoPanel;
    public GameObject spawnLoc;
    public List<BuildingInfo> thisBuilding;

    SelectionInfo _selectionInfo;
    LockInteractions _lockInteractions;
    bool unlockable;

    void Start()
    {
        _selectionInfo = infoPanel.GetComponent<SelectionInfo>();

        thisBuilding[0].name = thisBuilding[0].myBuilding.name;
        thisBuilding[0].myFood = thisBuilding[0].myBuilding.foodStat;
        thisBuilding[0].myMats = thisBuilding[0].myBuilding.materialStat;
        thisBuilding[0].myMoney = thisBuilding[0].myBuilding.moneyStat;
        thisBuilding[0].myCitizens = thisBuilding[0].myBuilding.citizenStat;
        thisBuilding[0].buildingModel = thisBuilding[0].myBuilding.buildingModel;
    }

    void UnlockCheck()
    {
        if (thisLock.materialCost <= CivManager.instance.mats && thisLock.moneyCost <= CivManager.instance.money && thisLock.citizenCost <= CivManager.instance.people)
        {
            LockInteractions.canUnlock = true;
            unlockable = true;
        }
        else
        {
            LockInteractions.canUnlock = false;
            unlockable = false;
        }
    }

    void OnMouseEnter()
    {
        infoPanel.SetActive(true);
        _selectionInfo.currLock = thisLock;
        _selectionInfo.Information();
        UnlockCheck();
    }

    void OnMouseExit()
    {
        infoPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        if(unlockable)
        {
            CivManager.instance.RemoveIncome(thisLock.materialCost, CivManager.Type.Mats);
            CivManager.instance.RemoveIncome(thisLock.moneyCost, CivManager.Type.Money);
            CivManager.instance.RemoveIncome(thisLock.citizenCost, CivManager.Type.People);

            //thisBuilding[0].buildingModel.SetActive(true);
            ObjectPooler.instance.GetFromPool("Hammer", spawnLoc.transform.position, Quaternion.Euler(0, spawnLoc.transform.localEulerAngles.y, 0));

            BuildingManager.instance.AddBuilding(thisBuilding);
            Destroy(transform.root.gameObject);
        }
        
    }
}
