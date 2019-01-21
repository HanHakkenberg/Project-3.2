using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour {
    [SerializeField] TransformReference mainCam;

    void Update() {
        transform.LookAt(mainCam.Value);
    }
}