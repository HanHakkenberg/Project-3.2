using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class IslandCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera buildingCam;
    public CinemachineVirtualCamera mainCam;
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private Building thisBuilding;
    Animator myAnimator;
    public PlayableDirector buildingAnim;
    public bool isPlaced;

    private void Start ()
    {
        mainCam = MainCamAssign.instance.mainCamera;
        infoCanvas = MainCamAssign.instance.infoCanvas;
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
        if(Input.GetButtonDown("Jump"))
        {
            FirstPlaced();
        }
    }

    void OnMouseDown ()
    {
        Debug.Log("clicked");
        if(isPlaced)
        {
        mainCam.enabled = false;
        buildingCam.enabled = true;
        BuildingDisplay.building = thisBuilding;
        myAnimator.SetBool ("Fading", true);
        infoCanvas.GetComponentInParent<BuildingDisplay> ().DisplayInfo ();
        }
    }
    public void FirstPlaced()
    {
        if(BuildingManager.instance.firstBuildings[thisBuilding.buildingNumb] == true)
        {
            buildingAnim.Play();

            BuildingManager.instance.firstBuildings[thisBuilding.buildingNumb] = false;
            isPlaced = true;
        }
    }
}
