using UnityEngine;

public class SetTransform : MonoBehaviour {
    [SerializeField] CameraReference myTransform;

    void Awake() {
        myTransform.Value = GetComponent<Camera>();
    }

    void Start(){
        myTransform.Value = GetComponent<Camera>();
    }

    void OnEnable() {
        myTransform.Value = GetComponent<Camera>();
    }
}
