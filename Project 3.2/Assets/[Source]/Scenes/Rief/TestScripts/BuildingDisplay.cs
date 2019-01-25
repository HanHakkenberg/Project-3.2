using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingDisplay : MonoBehaviour
{

    public static Building building;
    public static int currNumb;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    [SerializeField] TextMeshProUGUI displayUsedCitizens;
    [SerializeField] TextMeshProUGUI displayFood;
    [SerializeField] TextMeshProUGUI displayMaterials;
    [SerializeField] TextMeshProUGUI displayMoney;
    [SerializeField] TMP_InputField citizenDisplay;
    [SerializeField] int myCitizens;

    [SerializeField] GameObject upgradePanel;
    [SerializeField] TextMeshProUGUI materialsCostText;
    [SerializeField] TextMeshProUGUI moneyCostText;

    int upgradeAmount;

    public void DisplayInfo()
    {
        if (building != null)
        {
            myCitizens = BuildingManager.instance.allBuildings[currNumb].myCitizens;
            nameText.text = building.name;
            descriptionText.text = building.description;

            if(myCitizens >= 10)
            {
                myCitizens = 10;
            }
            citizenDisplay.text = myCitizens.ToString ();
            displayUsedCitizens.text = "Citizens: " + myCitizens + " working, " + (CivManager.instance.people - CivManager.instance.usedPeople) + " left.";
            displayFood.text = "Food: " + (building.foodStat * CivManager.instance.stabilityModifier) * myCitizens;
            displayMaterials.text = "Materials: " + (building.materialStat * CivManager.instance.stabilityModifier) * myCitizens;
            displayMoney.text = "Money: " + (building.moneyStat * CivManager.instance.stabilityModifier) * myCitizens;
            
        }
    }

    void StatChange()
    {
        BuildingManager.instance.allBuildings[currNumb].myFood = Mathf.RoundToInt((building.foodStat) * CivManager.instance.stabilityModifier) * myCitizens;
        BuildingManager.instance.allBuildings[currNumb].myMats = Mathf.RoundToInt((building.materialStat) * CivManager.instance.stabilityModifier) * myCitizens;
        BuildingManager.instance.allBuildings[currNumb].myMoney = Mathf.RoundToInt((building.moneyStat) * CivManager.instance.stabilityModifier) * myCitizens;
        BuildingManager.instance.allBuildings[currNumb].myCitizens = myCitizens;
        BuildingManager.instance.AddStats();
    }

    /*public void HoverUpgrade()
    {
        upgradePanel.SetActive(true);
        moneyCostText.text = "Money: " + BuildingManager.instance.allBuildings[currNumb].upgradeMoneyCost;
        materialsCostText.text = "Materials: " + BuildingManager.instance.allBuildings[currNumb].upgradeMatsCost;
    }
    public void HoverOff()
    {
        upgradePanel.SetActive(false);
    }
    public void Upgrade()
    {
        print("click");
        if (BuildingManager.instance.allBuildings[currNumb].upgradeMatsCost <= CivManager.instance.mats && BuildingManager.instance.allBuildings[currNumb].upgradeMoneyCost <= CivManager.instance.money)
        {
            print("work");
            BuildingManager.instance.allBuildings[currNumb].myFood *= 2;
            BuildingManager.instance.allBuildings[currNumb].myMats *= 2;
            BuildingManager.instance.allBuildings[currNumb].myMoney *= 2;
            DisplayInfo();
            BuildingManager.instance.AddStats();
        }
    }*/
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
                int newUsedCitizens;
                newUsedCitizens = myCitizens -= assignedCitizens;
                myCitizens = assignedCitizens;
                CivManager.instance.usedPeople -= newUsedCitizens;
            }
        }
        StatChange();
        DisplayInfo ();
    }

    public void CitizenInfo() //when you change the value, this starts.
    {
        
       int newCitizens;
        if (int.TryParse (citizenDisplay.text, out newCitizens) || citizenDisplay.text == string.Empty)
        {
            int extraCitizens = newCitizens - myCitizens;
            displayUsedCitizens.text = "Citizens: " + myCitizens + " (" + extraCitizens + ")" + " working " + (CivManager.instance.people - CivManager.instance.usedPeople) + " (" + -extraCitizens + ")" + " left.";
            displayFood.text = "Food: " + (building.foodStat * CivManager.instance.stabilityModifier) * myCitizens + " (" + (building.foodStat * CivManager.instance.stabilityModifier) *extraCitizens + ")";
            displayMaterials.text = "Materials: " + (building.materialStat * CivManager.instance.stabilityModifier) * myCitizens + " (" + (building.materialStat * extraCitizens * CivManager.instance.stabilityModifier) + ")";
            displayMoney.text = "Money: " + (building.moneyStat * CivManager.instance.stabilityModifier) * myCitizens + " (" + (building.moneyStat * extraCitizens * CivManager.instance.stabilityModifier) + ")";
        }
        
    }
}
