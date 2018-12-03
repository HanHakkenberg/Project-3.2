using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMapObject : MonoBehaviour {
    [SerializeField] GameObject spottedObject;

    void Start() {
        spottedObject.SetActive(false);
    }
}