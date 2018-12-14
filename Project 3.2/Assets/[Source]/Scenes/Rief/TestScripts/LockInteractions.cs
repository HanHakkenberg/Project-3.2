using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

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
    public TextMeshProUGUI unlockText;
    bool unlockable = false;
    public PlayableDirector buildingAnim;
    

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
        print(unlockable);
    }
    void OnMouseEnter()
    {
        myAnim.SetBool("Hovering", true);
        costPanel.SetActive(true);
        if(myLock.materialCost <= CivManager.instance.mats && myLock.moneyCost <= CivManager.instance.money && myLock.citizenCost <= CivManager.instance.people)
        {
            unlockable = true;
            unlockText.text = "Unlockable!";
        }
        else
        {
            unlockable = false;
            unlockText.text = "Not enough resources!";
        }
    }

    void OnMouseExit()
    {
        myAnim.SetBool("Hovering", false);
        costPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        if(unlockable)
        {
            buildingAnim.Play();
            //this.gameObject.SetActive(false);
        }
    }
}
