using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour {
    [SerializeField] Camera Target;

    [SerializeField] FloatReference fieldOfView;
    [SerializeField] BoolReference physicalCamera;

    [SerializeField] FloatReference clippingPlaneNear, clippingPlaneFar;

    [SerializeField] TransformReference currentCamera;
    [SerializeField] bool setCameraAtStart;

    void Start() {
        if(setCameraAtStart) {
            SetMainCamera();
        }
    }

    public void UpdateClippingPlaneFar() {
        Target.farClipPlane = clippingPlaneFar.Value;
    }

    public void UpdateClippingPlaneNear() {
        Target.nearClipPlane = clippingPlaneNear.Value;
    }

    public void UpdateFieldOfView() {
        Target.fieldOfView = fieldOfView.Value;
    }

    public void UpdatePhysicalCamera() {
        //Target.ph = physicalCamera.Value;
    }

    public void SetMainCamera() {
        currentCamera.Value = transform;
    }
}