using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionInfo : MonoBehaviour
{
    public Building currBuilding;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI description;

    public TextMeshProUGUI statOne;
    public TextMeshProUGUI statTwo;
    public TextMeshProUGUI statThree;
    public Sprite unlockSprite;


    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Information()
    {
        name.text = currBuilding.name;
        description.text = currBuilding.costDescription;
        statOne.text = "Materials: " + currBuilding.materialCost;
        statTwo.text = "Money: " + currBuilding.moneyCost;
        statThree.text = "Citizens: " + currBuilding.citizenCost;
    }

    void Unlockable()
    {

    }
}
