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
            //cam = GetComponent<Camera>();
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                cam = Camera.main;
            }
        }
        
        void Update()
        {
            gridBaker = FindObjectOfType<GridBaker>();
            if (gridBaker == null)
            {
                Debug.LogError("No GridBaker in Scene");
                return;
            }

            //mousePos = GlobalVariables.MouseWorldPosition(layerMask);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, cam.farClipPlane, layerMask))
            {
                mousePos = hit.point;
            }
            else
            {
                return;
            }
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (!placingObject)
                {
                    objectWerePlacing = Instantiate(buildingPrab, mousePos, Quaternion.identity, buildingHolder).transform;
                    placingObject = true;
                }
                else
                {
                    //RaycastHit groundCheckHit;
                    //if (Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z),
                        //-Vector3.up, out groundCheckHit, Mathf.Infinity, layerMask))
                    if(GroundCheck(out RaycastHit groundCheckHit))
                    {
                        CellObject cellObjHolder = groundCheckHit.transform.GetComponent<CellObject>();

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
                //RaycastHit groundCheckHit;
                //if(Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z), -Vector3.up, out groundCheckHit, Mathf.Infinity, layerMask))
                if(GroundCheck(out RaycastHit groundCheckHit))
                {
                    CellObject cellObjHolder = groundCheckHit.transform.GetComponent<CellObject>();

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
                                objectWerePlacing.position = groundCheckHit.point;
                            }
                        }
                        else
                        {
                            objectWerePlacing.position = groundCheckHit.point;
                            Debug.Log("cellObj.cell is null");
                        }
                    }
                    else
                    {
                        objectWerePlacing.position = groundCheckHit.point;
                        Debug.Log("cellObj is null");
                    }
                }
            }
        }

        bool GroundCheck(out RaycastHit groundCheckHit)
        {
            return (Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z),
                -Vector3.up, out groundCheckHit, Mathf.Infinity, layerMask));
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(mousePos, 1f);
            
        }
    }
}
