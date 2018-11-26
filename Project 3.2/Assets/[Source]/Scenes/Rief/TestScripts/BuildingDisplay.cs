using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingDisplay : MonoBehaviour
{
    
    public static Building building;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public TextMeshProUGUI statDisplayOne;
    public TextMeshProUGUI statDisplayTwo;
    public TextMeshProUGUI statDisplayThree;

    void Update ()
    {
        DisplayInfo ();
    }

    public void DisplayInfo()
    {
        nameText.text = building.name;
        descriptionText.text = building.description;

        statDisplayOne.text = building.tempStatOne.ToString ();
        statDisplayTwo.text = building.tempStatTwo.ToString();
        statDisplayThree.text = building.tempStatThree.ToString();
    }
}
