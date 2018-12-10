using UnityEngine;

public class InteractableMapObject : MonoBehaviour {
    [SerializeField] GameObject spottedObject;

    void Start() {
        if (spottedObject != null) {
            spottedObject.SetActive(false);
        }
    }
}