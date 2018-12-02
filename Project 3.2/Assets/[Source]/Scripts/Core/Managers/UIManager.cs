using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    #region RecourseUI
    // Used to show recourse values
    public TMP_Text money;
    public TMP_Text materials;
    public TMP_Text stability;
    public TMP_Text food;
    public TMP_Text people;
    //Used for feedback when value changes
    public TMP_Text moneyUpdate;
    public TMP_Text materialsUpdate;
    public TMP_Text foodUpdate;
    public TMP_Text poepleUpdate;
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
    public void UpdateResourceUI()
    {
        money.text = CivManager.instance.money.ToString();
        materials.text = CivManager.instance.buildingMaterials.ToString();
        stability.text = CivManager.instance.stability.ToString();
        food.text = CivManager.instance.food.ToString();
        people.text = CivManager.instance.poeple.ToString();

        // posible animation for UI scale
        // switch (CivManager.instance.stability)
        // {
        //     case -2:
        //     break;
        //     case -1:
        //     break;
        //     case 1:
        //     break;
        //     case 2:
        //     break;
        //     default:
        //     break;
        // }
    }

    public void RecourceUIPopup(int value , CivManager.Type type)
    {
        switch (type)
      {
         case CivManager.Type.BuildingMaterials:
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
            if (value > 0)
            {
                foodUpdate.text = "+" + value.ToString();
            }
            else
            {
                foodUpdate.text = value.ToString();                
            }
            break;
         case CivManager.Type.Poeple:
            if (value > 0)
            {
                poepleUpdate.text = "+" + value.ToString();
            }
            else
            {
                poepleUpdate.text = value.ToString();              
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
    }
}
