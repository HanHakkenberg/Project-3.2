using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        Invoke("AddValue", 2);
        Invoke("RemoveValue", 5);
    }

    void Update()
    {
        
    }

    void AddValue()
    {
        print("Adding value");
        CivManager.instance.AddIncome(100,CivManager.Type.Food);
    }

    void RemoveValue()
    {
        print("Removing value");
        CivManager.instance.RemoveIncome(100,CivManager.Type.Food);
    }
}
