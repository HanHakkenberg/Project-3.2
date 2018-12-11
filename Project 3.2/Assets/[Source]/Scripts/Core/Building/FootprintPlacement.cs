using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif
using Vector3 = UnityEngine.Vector3;

namespace Core.Building
{
    [AddComponentMenu("Core/Building/Footprint Placement")]
    public class FootprintPlacement : MonoBehaviour
    {
        //TODO: Blueprint Rotation 
        //TODO: Max Limit Height Difference.
        
        #region Variables

        #region Serialized

        public Construction construction; //[SerializeField] Construction construction;

        public LayerMask layerMask; //[SerializeField] LayerMask layerMask;
        
        [Tooltip("Transform which holds the spawned buildings")]
        #if UNITY_EDITOR
        [SceneObjectsOnly]
        #endif
        public Transform buildingHolder; //[SerializeField] Transform buildingHolder;
        
        #region Editor Buttons

        #if UNITY_EDITOR
        
        [ContextMenu("Toggle Debugging")]
        void ToggleDebugging()
        {
            Debug.Log(string.Format("Debugging set to: {0}", Convert.ToString(!debugging)));
            debugging = !debugging;
        }
            
        [ContextMenu("Toggle Gizmos")]
        void ToggleGizmos()
        {
            Debug.Log(string.Format("Gizmos set to: {0}", Convert.ToString(!displayGizmos)));
            displayGizmos = !displayGizmos;
        }

        #endif
        
        #endregion

        #endregion

        #region Non-Serialized

        [NonSerialized] public bool debugging = true, displayGizmos = true;
        
        private Vector3 mousePos = Vector3.zero;
        
        private Camera cam;

        private GridBaker gridBaker;

        private bool placingObject;

        private Transform objectWerePlacing;

        #endregion
        
        #endregion

        #region Methods

        void Start()
        {
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                cam = Camera.main;
            }
            
            if(debugging){Debug.Log("Placement - Start");}
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
            
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red, 10f);
            
            //Debug.Log(string.Format("<color(blue)> Placement layermask = {0}", layerMask.value));

            //maxDistance: cam.farClipPlane, layerMask: layerMask
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
                if(debugging){Debug.Log("Placement - Fire1");}

                if (!placingObject)
                {
                    if(debugging){Debug.Log("Placement - Instantiate Building");}

                    Debug.Log(string.Format("Construction = {0}", construction.name));

                    objectWerePlacing = Instantiate(construction.Prefab, mousePos, Quaternion.identity, buildingHolder)
                        .transform;
                    
                    placingObject = true;
                }
                else if(GroundCheck(out RaycastHit groundCheckHit, out Cell cell))
                {
                    if (cell != null)
                    {
                        if(debugging){Debug.Log("Placement - Trying to Anchor Building");}

                        //bool canPlaceBlueprint = FootprintCheck(cell.Position, out Vector3 blueprintPosition); //out float height);
                        
                        //objectWerePlacing.position = blueprintPosition;
                        
                        //if (canPlaceBlueprint)
                        if(FootprintCheck(cell.Position, out Vector3 blueprintPosition))
                        {
                            if(debugging){Debug.Log(string.Format("Placement - Anchored Building at {0}", cell.Position.ToString()));}

                            objectWerePlacing.position = blueprintPosition;

                            ToggleFootprintCells(cell.Position, AvailabilityState.Unavailable);
    
                            objectWerePlacing = null;
                            placingObject = false;
                        }
                    }
                }
            }

            if (placingObject)
            {
                if(GroundCheck(out RaycastHit groundCheckHit, out Cell cell))
                {
                    if (cell != null)
                    {
                        bool canPlaceBlueprint = FootprintCheck(cell.Position, out Vector3 blueprintPosition);
                        
                        objectWerePlacing.position = blueprintPosition;
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

        bool FootprintCheck(Vector3 checkPositionCenter, out Vector3 blueprintPosition)
        {
            Vector3 startingPosition = checkPositionCenter + new Vector3(-(((float)construction.footPrint.GetLength(0) * gridBaker.cellSize) / 2f)
                                                       , 0, -(((float)construction.footPrint.GetLength(1) * gridBaker.cellSize) / 2f)); //EDIT: Transform.position

            float highestHeight = checkPositionCenter.y;
            
            Vector3 checkPosition = new Vector3(0, gridBaker.skyLimit, 0);
            
            RaycastHit cellCheckHit;
            
            for (int x = 0; x <= construction.footPrint.GetLength(0) -1; x++)
            {
                checkPosition.x = startingPosition.x + ((x * gridBaker.cellSize) + (gridBaker.cellSize/2f));
                
                for (int z = 0; z <= construction.footPrint.GetLength(1) -1; z++)
                {
                    checkPosition.z = startingPosition.z + ((z * gridBaker.cellSize) + (gridBaker.cellSize/2f));
                    string color = "pink";
                    
                    if (construction.footPrint[x, z] == true)
                    {
                        color = "yellow";
                        
                        if (Physics.Raycast(checkPosition, -Vector3.up, out cellCheckHit, Mathf.Infinity, layerMask))
                        {
                            CellObject cellObject = cellCheckHit.transform.GetComponent<CellObject>();
                            Cell cell = null;
    
                            if (cellObject != null)
                            {
                                cell = cellObject.cell;
                            }
                            else
                            {
                                blueprintPosition = new Vector3(cell.Position.x, highestHeight, cell.Position.z);
                                
                                return false;
                            }
    
                            if (cell != null)
                            {
                                if (cell.Position.y >= highestHeight){highestHeight = cell.Position.y;}
                                
                                blueprintPosition = new Vector3(cell.Position.x, highestHeight, cell.Position.z);
                                
                                if (cell.Availability != AvailabilityState.Available)
                                {
                                    color = "red";

                                    return false;
                                }
                                else
                                {
                                    color = "blue";
                                    
                                    //DebugExtension.DebugBounds(new Bounds(cellCheckHit.point, Vector3.one), 50f);
                                }
                            }
                            else
                            {
                                blueprintPosition = new Vector3(cell.Position.x, highestHeight, cell.Position.z);
                                
                                return false;
                            }
                        }
                        else
                        {
                            blueprintPosition = checkPositionCenter;
                            
                            return false;
                        }
                    }
                    
                    //if(debugging){Debug.Log(string.Format("<color={3}>({0},{1})" + ((construction.footPrint[x, z] == true)? " Position = {2}</color>" : "</color>"),
                        //Convert.ToString(x), Convert.ToString(z), checkPosition.ToString(), color));}
                }
            }
            
            //if(debugging){Debug.Log("Can Place Building");}

            blueprintPosition = new Vector3(checkPositionCenter.x, highestHeight, checkPositionCenter.z);
            
            return true;
        }
        
        void ToggleFootprintCells(Vector3 checkPositionCenter, AvailabilityState state)
        {
            Vector3 startingPosition = checkPositionCenter + new Vector3(-(((float)construction.footPrint.GetLength(0) * gridBaker.cellSize) / 2f)
                                                       , 0, -(((float)construction.footPrint.GetLength(1) * gridBaker.cellSize) / 2f)); //EDIT: Transform.position
             
            Vector3 checkPosition = new Vector3(0, gridBaker.skyLimit, 0);
            
            for (int x = 0; x <= construction.footPrint.GetLength(0) -1; x++)
            {
                checkPosition.x = startingPosition.x + ((x * gridBaker.cellSize) + (gridBaker.cellSize/2f));
                
                for (int z = 0; z <= construction.footPrint.GetLength(1) -1; z++)
                {
                    checkPosition.z = startingPosition.z + ((z * gridBaker.cellSize) + (gridBaker.cellSize/2f));
                    
                    if (construction.footPrint[x, z] == true)
                    {
                        RaycastHit cellCheckHit; //EDIT: Move up?
    
                        if (Physics.Raycast(checkPosition, -Vector3.up, out cellCheckHit, Mathf.Infinity, layerMask))
                        {
                            CellObject cellObject = cellCheckHit.transform.GetComponent<CellObject>();
                            Cell cell = null;

                            if (cellObject != null)
                            {
                                cell = cellObject.cell;
                            }

                            if (cell != null)
                            {
                                cell.Availability = state;
                            }
                        }
                    }
                }
            }
        }
        
        void OnDrawGizmos()
        {
            if (displayGizmos)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(mousePos, 1f);
            }
        } 

        #endregion
       
    }
}
