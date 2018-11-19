using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour {
	[SerializeField] Camera Target;

	[SerializeField] FloatReference fieldOfView;
	[SerializeField] BoolReference physicalCamera;

	[SerializeField] FloatReference clippingPlaneNear, clippingPlaneFar;

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
		Target.usePhysicalProperties = physicalCamera.Value;
	}
}