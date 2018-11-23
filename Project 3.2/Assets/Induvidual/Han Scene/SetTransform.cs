using UnityEngine;

public class SetTransform : MonoBehaviour {
    [SerializeField] TransformReference myTransform;

    void Awake() {
        myTransform.Value = transform;
    }
}
