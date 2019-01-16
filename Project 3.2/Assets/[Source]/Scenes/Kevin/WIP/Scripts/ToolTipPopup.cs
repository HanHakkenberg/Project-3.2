using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipPopup : MonoBehaviour
{
    public static GameObject toolTipPannel;
    Transform toolTip;
    public static TMP_Text toolTipText;
    public string toolTipString;

    void Start() 
    {
        toolTipPannel = GameObject.FindGameObjectWithTag("ToolTip");
        toolTip = toolTipPannel.transform;
        toolTipText = toolTip.GetComponentInChildren<TMP_Text>();
    }
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
