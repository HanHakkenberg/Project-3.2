using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IslandCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera buildingCam;
    public Camera mainCam;
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private Building thisBuilding;

    void Update ()
    {
        if (Input.GetButtonDown ("Cancel"))
        {
            mainCam.enabled = true;
            buildingCam.enabled = false;
            infoCanvas.SetActive (false);
        }
    }

    void OnMouseDown ()
    {
        mainCam.enabled = false;
        buildingCam.enabled = true;
        infoCanvas.SetActive (true);
        BuildingDisplay.building = thisBuilding;
    }
}
