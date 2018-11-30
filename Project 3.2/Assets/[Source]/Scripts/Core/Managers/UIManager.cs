using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text money;
    public TMP_Text materials;
    public TMP_Text stability;
    public TMP_Text food;
    public TMP_Text people;

    void Start() 
    {
        UpdateResourceUI();
    }
    public void UpdateResourceUI()
    {
        money.text = CivManager.instance.money.ToString();
        materials.text = CivManager.instance.buildingMaterials.ToString();
        stability.text = CivManager.instance.stability.ToString();
        food.text = CivManager.instance.food.ToString();
        people.text = CivManager.instance.poeple.ToString();
    }
}
