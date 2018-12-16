using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelection : MonoBehaviour
{
    public IslandLock thisLock;
    public GameObject infoPanel;
    SelectionInfo _selectionInfo;
    LockInteractions _lockInteractions;
    bool unlockable;

    void Start()
    {
        _selectionInfo = infoPanel.GetComponent<SelectionInfo>();
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
        //infoPanel.SetActive(false);
        if(unlockable)
        {
            Destroy(transform.parent.transform.parent.gameObject);
        }
        
    }
}
