using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelection : MonoBehaviour
{
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
        if (thisBuilding[0].myBuilding.materialCost <= CivManager.instance.mats && thisBuilding[0].myBuilding.moneyCost <= CivManager.instance.money && thisBuilding[0].myBuilding.citizenCost <= CivManager.instance.people)
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
        _selectionInfo.currBuilding = thisBuilding[0].myBuilding;
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
            CivManager.instance.RemoveIncome(thisBuilding[0].myBuilding.materialCost, CivManager.Type.Mats);
            CivManager.instance.RemoveIncome(thisBuilding[0].myBuilding.moneyCost, CivManager.Type.Money);
            CivManager.instance.RemoveIncome(thisBuilding[0].myBuilding.citizenCost, CivManager.Type.People);

            ObjectPooler.instance.GetFromPool("Hammer", spawnLoc.transform.position, Quaternion.Euler(0, spawnLoc.transform.localEulerAngles.y, 0));

            BuildingManager.instance.AddBuilding(thisBuilding);
            Destroy(transform.root.gameObject);
        }
    }
}
