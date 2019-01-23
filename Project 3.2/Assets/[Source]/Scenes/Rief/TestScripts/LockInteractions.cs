using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class LockInteractions : MonoBehaviour
{
    Animator myAnim;
    public GameObject selectionPanel;
    public bool isActive = false;

    public bool canUnlock = false;
    public Sprite lockStatus;
    public Sprite locked;
    public Sprite unlocked;
    public AudioSource soundClick;


    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        LockStatus();
    }
    void OnMouseEnter()
    {
        if (!isActive)
        {
            myAnim.SetBool("Hovering", true);
        }
    }

    void OnMouseExit()
    {
        myAnim.SetBool("Hovering", false);
    }

    void OnMouseDown()
    {
        if (!isActive)
        {
            soundClick.Play();
        }
        isActive = true;
        selectionPanel.SetActive(true);
        
    }

    public void TurnOffActive()
    {
        isActive = false;
    }

    public void LockStatus()
    {
        ///Changes sprites of the lock based if you can unlock or not.
        if(canUnlock)
        {
            lockStatus = unlocked;
        }
        else
        {
            lockStatus = locked;
        }
        GetComponent<Image>().sprite = lockStatus;
    }
}
