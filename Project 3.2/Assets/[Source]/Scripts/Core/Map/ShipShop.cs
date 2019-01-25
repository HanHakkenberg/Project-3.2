using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShop : MonoBehaviour {
    [SerializeField] BoxCollider l;
    [SerializeField] LayerMask boatMask;
    [SerializeField] int sloopValueGold = 50;
    [SerializeField] int sloopValueMaterial = 75;

    [SerializeField] int schoonerValueGold = 80;
    [SerializeField] int schoonerValueMaterial = 125;

    public void CheckSloop(){
        if(!Physics.CheckBox(transform.position,l.size/2,transform.rotation, boatMask) && CivManager.instance.mats >= sloopValueMaterial && CivManager.instance.money >= sloopValueGold)
        {
            CivManager.instance.RemoveIncome(sloopValueMaterial,CivManager.Type.Mats);
            CivManager.instance.RemoveIncome(sloopValueGold,CivManager.Type.Money);
            ObjectPooler.instance.GetFromPool("Sloop",transform.position,transform.rotation);
        }
    }

    public void CheckSchooner(){
        if(!Physics.CheckBox(transform.position,l.size/2,transform.rotation, boatMask) && CivManager.instance.mats >= schoonerValueMaterial && CivManager.instance.money >= schoonerValueGold)
        {
            CivManager.instance.RemoveIncome(schoonerValueMaterial,CivManager.Type.Mats);
            CivManager.instance.RemoveIncome(schoonerValueGold,CivManager.Type.Money);
            ObjectPooler.instance.GetFromPool("Schooner",transform.position,transform.rotation);
        }
    }
}
