using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelection : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject spawnLoc;
    public LockInteractions _lockInteractions;
    public AudioSource placementSound;
    public List<BuildingInfo> thisBuilding;

    SelectionInfo _selectionInfo;
    bool unlockable;

    void Start()
    {
        _selectionInfo = infoPanel.GetComponent<SelectionInfo>();

        thisBuilding[0].name = thisBuilding[0].myBuilding.name;
        thisBuilding[0].myFood = thisBuilding[0].myBuilding.foodStat;
        thisBuilding[0].myMats = thisBuilding[0].myBuilding.materialStat;
        thisBuilding[0].myMoney = thisBuilding[0].myBuilding.moneyStat;
        thisBuilding[0].myCitizens = thisBuilding[0].myBuilding.citizenStat;
    }

    void UnlockCheck()
    {
        if (thisBuilding[0].myBuilding.materialCost <= CivManager.instance.mats && thisBuilding[0].myBuilding.moneyCost <= CivManager.instance.money && thisBuilding[0].myBuilding.citizenCost <= CivManager.instance.people)
        {
            _lockInteractions.canUnlock = true;
            unlockable = true;
        }
        else
        {
            _lockInteractions.canUnlock = false;
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
        _lockInteractions.canUnlock = false;
    }

    void OnMouseDown()
    {
        if(unlockable)
        {
            CivManager.instance.RemoveIncome(thisBuilding[0].myBuilding.materialCost, CivManager.Type.Mats);
            CivManager.instance.RemoveIncome(thisBuilding[0].myBuilding.moneyCost, CivManager.Type.Money);
            CivManager.instance.RemoveIncome(thisBuilding[0].myBuilding.citizenCost, CivManager.Type.People);

            thisBuilding[0].buildingModel.SetActive(true);

            BuildingManager.instance.AddBuilding(thisBuilding);
            placementSound.Play();
            Destroy(transform.root.gameObject);
        }
    }
}
