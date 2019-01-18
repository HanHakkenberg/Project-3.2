using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum Panels
    {
        Main,
        IslandInteraction,
        PauseMenu
    }
    #region Pannels
    GameObject activePannel;
    public GameObject mainPannel;
    public GameObject islandInteractionPannel;
    public GameObject pauseMenuPannel; 
    #endregion
    public static UIManager instance;

    #region RecourseUI
    // Used to show recourse values
    [SerializeField]
    TMP_Text money,materials,stability,food,people;
    //Used for feedback when value changes
    [SerializeField]
    TextMeshProUGUI moneyUpdate,materialsUpdate,foodUpdate,poepleUpdate,stabilityUpdate;
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
        mainPannel.SetActive(true);
    }

    public void SwitchPanel (Panels panels)
    {
        if (activePannel != null)
        {
            activePannel.SetActive(false);            
        }
        switch (panels)
        {
            case Panels.IslandInteraction:
            islandInteractionPannel.SetActive(true);
            activePannel = islandInteractionPannel;
            break;

            case Panels.PauseMenu:
            pauseMenuPannel.SetActive(true);
            activePannel = pauseMenuPannel;
            break;
        }
    }

    #region ResourceUI

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
        if (value > 0)
        {
            switch (type)
            {
                case CivManager.Type.Food:
                    foodUpdate.faceColor = new Color32(0,255,0,255);
                break;

                case CivManager.Type.Mats:
                    materialsUpdate.faceColor = new Color32(0,255,0,255);
                break;

                case CivManager.Type.Money:
                    moneyUpdate.faceColor = new Color32(0,255,0,255);
                break;

                case CivManager.Type.People:
                    poepleUpdate.faceColor = new Color32(0,255,0,255);
                break;

                case CivManager.Type.Stability:
                    stabilityUpdate.faceColor = new Color32(0,255,0,255);
                break;
            }
        }
        else
        {
            switch (type)
            {
                case CivManager.Type.Food:
                    foodUpdate.faceColor = new Color32(255,0,0,255);
                break;

                case CivManager.Type.Mats:
                    materialsUpdate.faceColor = new Color32(255,0,0,255);
                break;

                case CivManager.Type.Money:
                    moneyUpdate.faceColor = new Color32(255,0,0,255);
                break;
                case CivManager.Type.People:
                    poepleUpdate.faceColor = new Color32(255,0,0,255);
                break;
                case CivManager.Type.Stability:
                    stabilityUpdate.faceColor = new Color32(255,0,0,255);
                break;
            }
        }
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
    #endregion
}
