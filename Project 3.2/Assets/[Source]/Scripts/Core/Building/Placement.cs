using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Building
{
    [AddComponentMenu("Core/Building/Placement")]
    public class Placement : MonoBehaviour
    {
        
        //[AssetsOnly]
        //[SerializeField] private GameObject buildingPrab;
        //
        //[SerializeField] private LayerMask layerMask;
        //        
        //[Tooltip("Transform which holds the spawned buildings")]
        //[SceneObjectsOnly]
        //[SerializeField] private Transform buildingHolder;
        //
        //private Vector3 mousePos = Vector3.zero;
        //        
        //private Camera cam;
        //
        //private GridBaker gridBaker;
        //
        //private bool placingObject;
        //
        //private Transform objectWerePlacing;
        //
        //private CellObject cellObj;
        
        [AssetsOnly]
        public GameObject buildingPrab;

        public LayerMask layerMask;
        
        [Tooltip("Transform which holds the spawned buildings")]
        [SceneObjectsOnly]
        public Transform buildingHolder;

        private Vector3 mousePos = Vector3.zero;
        
        private Camera cam;

        private GridBaker gridBaker;

        private bool placingObject;

        private Transform objectWerePlacing;
        
        void Start()
        {
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                cam = Camera.main;
            }
            
            Debug.Log("Placement - Start");
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
                Debug.Log("Placement - Fire1");

                if (!placingObject)
                {
                    Debug.Log("Placement - Instantiate Building");

                    objectWerePlacing = Instantiate(buildingPrab, mousePos, Quaternion.identity, buildingHolder)
                        .transform;
                    
                    placingObject = true;
                }
                else if(GroundCheck(out RaycastHit groundCheckHit, out Cell cell))
                {
                    if (cell != null)
                    {
                        Debug.Log("Placement - Trying to Anchor Building");
    
                        if (cell.Availability == Cell.AvailabilityState.Available)
                        {
                            Debug.Log("Placement - Anchored Building");
                            
                            objectWerePlacing.position = cell.Position;
    
                            cell.Availability = Cell.AvailabilityState.Unavailable;
    
                            objectWerePlacing = null;
                            placingObject = false;
                        }
                    }
                }
            }

            if (placingObject)
            {
                if (GroundCheck(out RaycastHit groundCheckHit, out Cell cell))
                {
                    if (cell != null)
                    {
                        objectWerePlacing.position = cell.Position;
                        //Debug.Log("Placement: placingObject - Cell is NOT null");
                    }
                    else
                    {
                        objectWerePlacing.position = groundCheckHit.point;
                        //Debug.Log("Placement: placingObject - Cell IS null");
                    }
                }
            }
        }
        
        bool GroundCheck(out RaycastHit groundCheckHit, out Cell cell)
        {
            if(Physics.Raycast(new Vector3(mousePos.x, mousePos.y + gridBaker.skyLimit, mousePos.z),
                -Vector3.up, out groundCheckHit, Mathf.Infinity, layerMask))
            {
                CellObject cellObject = groundCheckHit.transform.GetComponent<CellObject>();

                if (cellObject != null)
                {
                    cell = cellObject.cell;

                    return true;
                }
                else
                {
                    cell = null;
                    
                    return false;
                }
            }
            else
            {
                cell = null;
                
                return false;
            }
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(mousePos, 1f);
            
        }
    }
}
