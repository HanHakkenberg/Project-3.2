using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraEvent : MonoBehaviour {
    [SerializeField] Canvas target;
    [SerializeField] TransformReference currentCamera;

    public void SetCamera() {
        target.worldCamera = currentCamera.Value.GetComponent<Camera>();
    }
}
