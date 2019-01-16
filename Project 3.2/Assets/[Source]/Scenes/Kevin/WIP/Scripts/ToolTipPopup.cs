using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipPopup : MonoBehaviour
{
    public static GameObject toolTipPannel;
    public static Transform toolTip;
    public static TMP_Text toolTipText;
    public string toolTipString;
    bool overUI;

    void Start() 
    {
        if(toolTipPannel == null)
        {
            toolTipPannel = GameObject.FindGameObjectWithTag("ToolTip");
            toolTip = toolTipPannel.transform;  
            toolTipPannel.SetActive(false);
            toolTipText = toolTip.GetComponentInChildren<TMP_Text>();
        }
    }

    void Update() 
    {
        if (overUI)
        {
            PointerOver();
        }
    }
    
    public void PointerEnter()
    {
        print("proent");
        toolTipPannel.SetActive(true);
        overUI = true;
        toolTipText.text = toolTipString;
    }

    public void PointerExit()
    {
        print("proent2");
        toolTipPannel.SetActive(false);
        overUI = false;
    }

    public void PointerOver()
    {
        print("proent3");
    	toolTip.position = Input.mousePosition + new Vector3(83,-40,0);
    }
}
