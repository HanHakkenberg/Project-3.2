using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwitch : MonoBehaviour {
    public static ShipSwitch instance;

    public List<ShipSwitchButton> ships = new List<ShipSwitchButton>();
    [SerializeField] int shipAmount;
    [SerializeField] GameObject shipSwitchButton;

    void Awake() {
        instance = this;

        for (int i = 0; i < shipAmount; i++) {
            GameObject button = Instantiate(shipSwitchButton, new Vector3(), new Quaternion(), transform);

            ShipSwitchButton newButton = button.GetComponent<ShipSwitchButton>();

            newButton.myIndex = i;
            newButton.gameObject.SetActive(false);

            ships.Add(newButton);
        }
    }

    public void SopttedObject(Transform index) {
        for (int i = 0; i < ships.Count; i++) {
            if (ships[i].myShip == index) {
                ships[i].spottingImage.SetActive(true);
                break;
            }
        }
    }

    public void Interactable(Transform index, bool State) {
        for (int i = 0; i < ships.Count; i++) {
            if (ships[i].myShip == index) {
                ships[i].interactButton.SetActive(State);
                break;
            }
        }
    }

    public void AddShip(Transform shipTransform) {
        for (int i = 0; i < ships.Count; i++) {
            if (ships[i].myShip == null) {
                ships[i].gameObject.SetActive(true);
                ships[i].myShip = shipTransform;
                return;
            }
        }
    }

    public void RemoveShip(Transform toRemove) {
        for (int i = 0; i < ships.Count; i++) {
            if (ships[i].myShip == toRemove) {
                ships[i].myShip = null;
                return;
            }
        }

    }
}