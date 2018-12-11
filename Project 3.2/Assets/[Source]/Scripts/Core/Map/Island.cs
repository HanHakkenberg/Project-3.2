using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

    public bool pillaged;
    public int foodLoot;
    public int matLoot;
    public int goldLoot;
    [SerializeField] TransformReference currentSelected;

    void OnMouseDown() {
        if (!Input.GetButton("Waypoint Interact")) {
            currentSelected.Value = transform;

            IslandInteractionManager.instance.IslandInsert(this);
        }
    }

    private void Start() 
    {
        PrototypeRandomizePillage();
    }

    void PrototypeRandomizePillage()
    {
        foodLoot = Random.Range(0,100);
        matLoot = Random.Range(0,100);
        goldLoot = Random.Range(0,100);
    }
}