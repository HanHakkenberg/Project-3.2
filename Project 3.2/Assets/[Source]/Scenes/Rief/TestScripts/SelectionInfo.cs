using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionInfo : MonoBehaviour
{
    public IslandLock currLock;
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
        name.text = currLock.name;
        description.text = currLock.description;
        statOne.text = "Materials: " + currLock.materialCost;
        statTwo.text = "Money: " + currLock.moneyCost;
        statThree.text = "Citizens: " + currLock.citizenCost;
    }

    void Unlockable()
    {

    }
}
