using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IslandCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera buildingCam;
    public CinemachineVirtualCamera mainCam;
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private Building thisBuilding;
    Animator myAnimator;

    private void Start ()
    {
        myAnimator = infoCanvas.GetComponent<Animator> ();
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
        mainCam.enabled = false;
        buildingCam.enabled = true;
        BuildingDisplay.building = thisBuilding;
        myAnimator.SetBool ("Fading", true);
        infoCanvas.GetComponentInParent<BuildingDisplay> ().DisplayInfo ();
    }
}
