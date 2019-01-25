using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShop : MonoBehaviour {
    [SerializeField] BoxCollider l;
    [SerializeField] LayerMask boatMask;
    [SerializeField] int sloopValue;

    public void CheckSloop(){
        if(!Physics.CheckBox(transform.position,l.size/2,transform.rotation, boatMask) && CivManager.instance.mats >= sloopValue)
        {

        }
    }
}
