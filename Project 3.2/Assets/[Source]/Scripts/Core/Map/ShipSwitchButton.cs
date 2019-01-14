using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwitchButton : MonoBehaviour {
    public Transform myShip;
    public GameObject spottingImage;
    public GameObject interactButton;
    public int myIndex;
    [SerializeField] Vector3Reference newCamPos;
    [SerializeField] BoolReference shipFollow;

    public void PressButton() {
        spottingImage.SetActive(false);
        interactButton.SetActive(false);
        shipFollow.Value = !shipFollow.Value;
        StartCoroutine(UpdateShipPos());
    }

    IEnumerator UpdateShipPos() {
        StopCoroutine(UpdateShipPos());

        while (shipFollow.Value) {
            newCamPos.Value = myShip.position;
            yield return null;
        }
    }
}