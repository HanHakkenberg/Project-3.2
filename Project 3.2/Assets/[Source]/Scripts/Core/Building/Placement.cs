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
        
        [Tooltip("Transform which holds the spawned buildings")]
        [SceneObjectsOnly]
        [SerializeField] private Transform buildingHolder;

        private Vector3 mousePos = Vector3.zero;
        
        private Camera cam;

        private GridBaker gridBaker;

        private bool placingObject;

        private Transform objectWerePlacing;

        private CellObject cellObj;
        
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
                    objectWerePlacing = Instantiate(buildingPrab, mousePos, Quaternion.identity, buildingHolder).transform;
                    placingObject = true;
                }
                else
                {

                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z),
                        -Vector3.up, out hit, Mathf.Infinity, layerMask))
                    {
                        CellObject cellObjHolder = hit.transform.GetComponent<CellObject>();

                        if (cellObjHolder != null)
                        {
                            if (cellObj.cell != null)
                            {
                                if (cellObj.cell.Availability == Cell.AvailabilityState.Available)
                                {
                                    //Debug.Log("Cell Available");
                                    objectWerePlacing.position = cellObj.transform.position;

                                    cellObj.cell.Availability = Cell.AvailabilityState.Unavailable;

                                    objectWerePlacing = null;
                                    placingObject = false;
                                }
                            }
                        }
                    }
                }
            }
           
            if (placingObject)
            {
                RaycastHit hit;
                if(Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z), -Vector3.up, out hit, Mathf.Infinity, layerMask))
                {
                    CellObject cellObjHolder = hit.transform.GetComponent<CellObject>();

                    if (cellObjHolder != null)
                    {
                        cellObj = cellObjHolder;
                        
                        //Debug.Log("cellObj is not null");
                        if (cellObj.cell != null)
                        {
                            if (cellObj.cell.Availability == Cell.AvailabilityState.Available)
                            {
                                //Debug.Log("Cell Available");
                                objectWerePlacing.position = cellObj.transform.position;
                            }
                            else
                            {
                                //Debug.Log("Cell Unavailable");
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
