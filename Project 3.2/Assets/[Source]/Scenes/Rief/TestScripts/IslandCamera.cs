using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class IslandCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera buildingCam;
    CinemachineVirtualCamera mainCam;
    [SerializeField] private GameObject infoCanvas;
    Animator myAnimator;
    public bool isPlaced;
    public int numb;

    private void Start ()
    {
        mainCam = MainCamAssign.instance.mainCamera;
        infoCanvas = MainCamAssign.instance.infoCanvas;
        myAnimator = infoCanvas.GetComponent<Animator> ();
        numb = BuildingManager.instance.buildingNum;
    }
    void Update ()
    {
        if (Input.GetButtonDown ("Cancel"))
        {
            mainCam.enabled = true;
            buildingCam.enabled = false;
            myAnimator.SetBool ("Fading", false);
        }
    }

    void OnMouseDown ()
    {
        infoCanvas.SetActive(true);
        if(infoCanvas.active == true)
        {
            mainCam.enabled = false;
            buildingCam.enabled = true;
            BuildingDisplay.building = BuildingManager.instance.allBuildings[numb].myBuilding;
            BuildingDisplay.currNumb = numb;
            myAnimator.SetBool ("Fading", true);
            infoCanvas.GetComponentInParent<BuildingDisplay> ().DisplayInfo ();
        }

    }
}
