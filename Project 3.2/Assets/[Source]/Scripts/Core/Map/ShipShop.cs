using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShop : MonoBehaviour {
    [SerializableField] BoxCollider l;
    [SerializableField] LayerMask boatMask;

    public void Check(){
        if(!Physics.CheckBox(transform.position,l.half,transform.rotation, boatMask) && Civmanager.)
        {

        }
    }
}
