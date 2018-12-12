using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockedLookAt : MonoBehaviour
{

    void Update()
    {
        LookAt();
    }

    void LookAt()
    {
        this.transform.LookAt(MainCamAssign.instance.mainCamera.transform);
    }

}
