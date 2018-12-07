using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    #region RecourseUI
    // Used to show recourse values
    [SerializeField]
    TMP_Text money,materials,stability,food,people;
    //Used for feedback when value changes
    [SerializeField]
    TMP_Text moneyUpdate,materialsUpdate,foodUpdate,poepleUpdate,stabilityUpdate;
    #endregion

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    void Start() 
    {
        UpdateResourceUI();
    }

    /// <summary>
    /// Only use this function if you need to refresh the ResourceUI
    /// It should do this automaticly using the ResourceUIPopup.
    /// </summary>
    public void UpdateResourceUI()
    {
        stability.text = CivManager.instance.stability.ToString();
        materials.text = CivManager.instance.mats.ToString();
        money.text = CivManager.instance.money.ToString();
        food.text = CivManager.instance.food.ToString();
        people.text = CivManager.instance.people.ToString();
    }

    /// <summary>
    /// This function manages the feedback values for resources and updates the actual value itself
    /// </summary>
    /// <param name="value">The value used in the income feedback text</param>
    /// <param name="type">The type of recourse it effects</param>
    public void RecourceUIPopup(int value , CivManager.Type type)
    {
        switch (type)
      {
            case CivManager.Type.Mats:
                materials.text = CivManager.instance.mats.ToString();
                if (value > 0)
                {
                    materialsUpdate.text = "+" + value.ToString();
                }
                else
                {
                    materialsUpdate.text = value.ToString();                
                }
                break;
            case CivManager.Type.Money:
                money.text = CivManager.instance.money.ToString();
                if (value > 0)
                {
                    moneyUpdate.text = "+" + value.ToString();
                }
                else
                {
                    moneyUpdate.text = value.ToString();                
                }
                break;
            case CivManager.Type.Food:
                food.text = CivManager.instance.food.ToString();
                if (value > 0)
                {
                    foodUpdate.text = "+" + value.ToString();
                }
                else
                {
                    foodUpdate.text = value.ToString();                
                }
                break;
            case CivManager.Type.People:
                people.text = CivManager.instance.people.ToString();
                if (value > 0)
                {
                    poepleUpdate.text = "+" + value.ToString();
                }
                else
                {
                    poepleUpdate.text = value.ToString();              
                }
                break;        
            case CivManager.Type.Stability:
                stability.text = CivManager.instance.stability.ToString();
                if (value > 0)
                {
                    stabilityUpdate.text = "+" + value.ToString();
                }
                else
                {
                    stabilityUpdate.text = value.ToString();       
                }
                break;    
      }
      Invoke("RecourseUIPopupReset", 2);
    }
    
    public void RecourseUIPopupReset()
    {
        materialsUpdate.text = "";
        moneyUpdate.text = "";
        foodUpdate.text = "";
        poepleUpdate.text = "";
        stabilityUpdate.text = "";
    }
}
