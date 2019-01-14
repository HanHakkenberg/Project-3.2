using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipPopup : MonoBehaviour
{
    public GameObject toolTipPannel;
    public static Transform toolTip;
    public static string toolTipText;

    void Update() 
    {
        toolTip.position = Input.mousePosition;    
    }

    void OnMouseEnter() 
    {
        toolTipPannel.SetActive(true);
    }

    void OnMouseExit() 
    {
        toolTipPannel.SetActive(false);        
    }
}
