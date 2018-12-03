using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCamAssign : MonoBehaviour
{
    
    public static MainCamAssign instance;
    public CinemachineVirtualCamera mainCamera;
    public GameObject infoCanvas;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
