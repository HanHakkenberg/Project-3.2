using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwitchButton : MonoBehaviour {
    public Transform myShip;
    public GameObject spottingImage;
    public GameObject interactButton;
    public int myIndex;

    public void PressButton() {
        spottingImage.SetActive(false);
        interactButton.SetActive(false);
    }
}