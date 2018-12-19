using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSelection : MonoBehaviour
{

    public GameObject panel;
    public GameObject theLock;
    void OnMouseDown()
    {
        //theLock.GetComponent<LockInteractions>().isActive = false;
        panel.SetActive(false);
        
    }
}
