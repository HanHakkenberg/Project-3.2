using System.Collections;
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

    void Update ()
    {
        DisplayInfo ();
    }

    public void DisplayInfo()
    {
        nameText.text = building.name;
        descriptionText.text = building.description;

        statDisplayOne.text = building.tempStatOne.ToString();
        statDisplayTwo.text = building.tempStatTwo.ToString();
        statDisplayThree.text = building.tempStatThree.ToString();
    }
}
