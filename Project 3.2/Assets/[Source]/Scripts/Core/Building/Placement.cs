using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Building
{
    [AddComponentMenu("Core/Building/Placement")]
    public class Placement : SerializedMonoBehaviour
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

        //public Construction construction;
        public GameObject buildingPrab;

        //public LayerMask layerMask;
        
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

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red, 10f);
            
            //if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: Mathf.Infinity, layerMask: 1 << 10)) //maxDistance: cam.farClipPlane, layerMask: layerMask
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: Mathf.Infinity))
            {
                mousePos = hit.point;
            }
            else
            {
                Debug.Log("Raycast didn't hit anything.");
                return;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Placement - Fire1");

                if (!placingObject)
                {
                    Debug.Log("Placement - Instantiate Building");

                    objectWerePlacing = Instantiate(buildingPrab, mousePos, Quaternion.identity, buildingHolder).transform;
                    
                    placingObject = true;
                }
                else if(GroundCheck(out RaycastHit groundCheckHit, out Cell cell))
                {
                    Debug.Log("GroundCheck hit something");
                    
                    if (cell != null)
                    {
                        Debug.Log("Placement - Trying to Anchor Building");
    
                        if (cell.Availability == AvailabilityState.Available)
                        {
                            Debug.Log("Placement - Anchored Building");
                            
                            objectWerePlacing.position = cell.Position;
    
                            cell.Availability = AvailabilityState.Unavailable;
    
                            objectWerePlacing = null;
                            placingObject = false;
                        }
                    }
                    else
                    {
                        Debug.Log("Placement - Cell is NULL");
                    }
                }
                else
                {                    
                    Debug.Log("GroundCheck hit NOTHING");
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
            Vector3 checkPos = new Vector3(mousePos.x, mousePos.y + 10000f, mousePos.z); //Mathf.Infinity
            
            Ray ray = new Ray(origin: checkPos, direction: -Vector3.up);
            
            //Debug.DrawRay(ray.origin, ray.direction * 50f, Color.green, 10f);
            Debug.Log(checkPos.ToString());
            Debug.Log(LayerMask.LayerToName(10));
            
            //if(Physics.Raycast(ray, out groundCheckHit, maxDistance: Mathf.Infinity))
            if(Physics.Raycast(ray, out groundCheckHit, maxDistance: Mathf.Infinity, layerMask: 1 << 10))
            //if(Physics.Raycast(ray, out groundCheckHit, Mathf.Infinity, ~(1 << 11)))
            {
                CellObject cellObject = groundCheckHit.transform.GetComponent<CellObject>();

                if (cellObject != null)
                {
                    Debug.Log("GroundCheck - Hit Contained a CellObject");
                    
                    cell = cellObject.cell;

                    return true;
                }
                else
                {
                    Debug.Log("GroundCheck - Hit DID NOT Contain a CellObject");
                    
                    cell = null;
                    
                    return false;
                }
            }
            else
            {
                Debug.Log("GroundCheck - Raycast hit Nothing");
                
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
