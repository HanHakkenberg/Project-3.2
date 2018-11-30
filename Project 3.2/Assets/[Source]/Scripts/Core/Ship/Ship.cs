using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    [HideInInspector] public List<Vector3> myPath = new List<Vector3>();
    [SerializeField] TransformReference currentSelected;

    void OnMouseDown() {
        
    }

    IEnumerator PathUpdate() {

        while(currentSelected.Value == gameObject) {


            yield return null;

        }
    }
}
