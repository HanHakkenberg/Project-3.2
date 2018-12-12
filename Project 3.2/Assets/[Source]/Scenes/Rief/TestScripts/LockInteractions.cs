using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockInteractions : MonoBehaviour
{
    Animator myAnim;
    public GameObject costPanel;
    public IslandLock myLock;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI materialText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI citizenText;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        nameText.text = myLock.name;
        descriptionText.text = myLock.description;
        materialText.text = "Materials: " + myLock.materialCost;
        moneyText.text = "Money: " + myLock.moneyCost;
        citizenText.text = "Citizens: " + myLock.citizenCost;
    }

    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        myAnim.SetBool("Hovering", true);
        costPanel.SetActive(true);
    }

    void OnMouseExit()
    {
        myAnim.SetBool("Hovering", false);
        costPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        if(myLock.materialCost <= CivManager.instance.mats && myLock.moneyCost <= CivManager.instance.money && myLock.citizenCost <= CivManager.instance.people)
        {
            //unlock
        }
    }
}
