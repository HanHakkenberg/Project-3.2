using UnityEngine;

public class SetTransform : MonoBehaviour {
    [SerializeField] TransformReference myTransform;

    void Awake() {
        myTransform.Value = transform;
    }

    void Start() {
        myTransform.Value = transform;
    }

    void OnEnable() {
        myTransform.Value = transform;
    }
}