using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheaterSceeter : MonoBehaviour
{
    void Update() 
    {
        UpdateCheat();
    }

    void UpdateCheat()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            print("test");
            AddResourcesCheat();
        }
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            RemoveResourcesCheat();
        }
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            TriggerEventCheat();
        }
    }

    void AddResourcesCheat()
    {
        CivManager.instance.AddIncome(100,CivManager.Type.Food);
        CivManager.instance.AddIncome(100,CivManager.Type.Money);
        CivManager.instance.AddIncome(100,CivManager.Type.Mats);
    }

    void RemoveResourcesCheat()
    {
        CivManager.instance.RemoveIncome(100,CivManager.Type.Food);
        CivManager.instance.RemoveIncome(100,CivManager.Type.Money);
        CivManager.instance.RemoveIncome(100,CivManager.Type.Mats);
    }

    void TriggerEventCheat()
    {
        EventManager.instance.TriggerRandomEvent();
    }
}
