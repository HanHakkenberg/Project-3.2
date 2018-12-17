﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingDisplay : MonoBehaviour
{

    public static Building building;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    [SerializeField] TextMeshProUGUI statDisplayOne;
    [SerializeField] TextMeshProUGUI statDisplayTwo;
    [SerializeField] TextMeshProUGUI statDisplayThree;
    [SerializeField] TMP_InputField citizenDisplay;
    [SerializeField] int myCitizens;

    public void DisplayInfo()
    {
        if (building != null)
        {
            nameText.text = building.name;
            descriptionText.text = building.description;

            citizenDisplay.text = myCitizens.ToString ();
            statDisplayOne.text = "Food: " + (building.foodStat * CivManager.instance.stabilityModifier) * myCitizens;
            statDisplayTwo.text = "Materials: " + (building.materialStat * CivManager.instance.stabilityModifier) * myCitizens;
            statDisplayThree.text = "Money: " + (building.moneyStat * CivManager.instance.stabilityModifier) * myCitizens;

        }
    }
    public void CitizenChange() //when you change the value and press enter, this starts.
    {

        if (citizenDisplay.text == string.Empty)
        {
            myCitizens = 0;
        }
        else
        {
            int assignedCitizens;
            if (int.TryParse (citizenDisplay.text, out assignedCitizens))
            {
                myCitizens = assignedCitizens;
            }
        }
        DisplayInfo ();
    }

    public void CitizenInfo() //when you change the value, this starts.
    {
        
       int newCitizens;
        if (int.TryParse (citizenDisplay.text, out newCitizens) || citizenDisplay.text == string.Empty)
        {
            int extraCitizens = newCitizens - myCitizens;
            statDisplayOne.text = "Food: " + (building.foodStat * CivManager.instance.stabilityModifier) * myCitizens + " (" + (building.foodStat * CivManager.instance.stabilityModifier) *extraCitizens + ")";
            statDisplayTwo.text = "Materials: " + (building.materialStat * CivManager.instance.stabilityModifier) * myCitizens + " (" + (building.materialStat * extraCitizens * CivManager.instance.stabilityModifier) + ")";
            statDisplayThree.text = "Money: " + (building.moneyStat * CivManager.instance.stabilityModifier) * myCitizens + " (" + (building.moneyStat * extraCitizens * CivManager.instance.stabilityModifier) + ")";
        }
        
    }
}
