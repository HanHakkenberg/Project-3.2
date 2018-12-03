using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Building
{
    [AddComponentMenu("Core/Building/Placement")]
    public class Placement : MonoBehaviour
    {
        
        [AssetsOnly]
        [SerializeField] private GameObject buildingPrab;

        [SerializeField] private LayerMask layerMask;

        private Vector3 mousePos = Vector3.zero;
        
        private Camera cam;

        private GridBaker gridBaker;

        private bool placingObject;

        private Transform objectWerePlacing;
        
        void Start()
        {
            cam = GetComponent<Camera>();
        }
        
        void Update()
        {
            gridBaker = FindObjectOfType<GridBaker>();
            if (gridBaker == null)
            {
                Debug.LogError("No GridBaker in Scene");
                return;
            }

            mousePos = GlobalVariables.MouseWorldPosition(layerMask);
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (!placingObject)
                {
                    objectWerePlacing = Instantiate(buildingPrab, mousePos, Quaternion.identity, transform).transform;
                    placingObject = true;
                }
                else
                {
                    objectWerePlacing = null;
                    placingObject = false;
                }
            }
           
            if (placingObject)
            {
                RaycastHit hit;
                if(Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z), -Vector3.up, out hit, Mathf.Infinity, layerMask))
                {
                    CellObject cellObj = hit.transform.GetComponent<CellObject>();

                    if (cellObj != null)
                    {
                        if (cellObj.cell != null)
                        {
                            if (cellObj.cell.Availability == Cell.AvailabilityState.Available)
                            {
                                objectWerePlacing.position = cellObj.transform.position;
                            }
                            else
                            {
                                objectWerePlacing.position = hit.point;
                            }
                        }
                        else
                        {
                            objectWerePlacing.position = hit.point;
                            Debug.Log("cellObj.cell is null");
                        }
                    }
                    else
                    {
                        objectWerePlacing.position = hit.point;
                        Debug.Log("cellObj is null");
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(mousePos, 1f);
            
        }
    }
}
