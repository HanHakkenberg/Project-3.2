using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        GetComponent<Button>().onClick.AddListener(() => {PointerExit();});
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
        toolTipPannel.SetActive(true);
        overUI = true;
        toolTipText.text = toolTipString;
    }

    public void PointerExit()
    {
        toolTipPannel.SetActive(false);
        overUI = false;
    }

    public void PointerOver()
    {
    	toolTip.position = Input.mousePosition;
    }
}
