using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

using Sirenix.OdinInspector;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

namespace Core.Building
{
    [AddComponentMenu("Core/Building/GridBaker")]
    public class GridBaker : MonoBehaviour
    {
        //TODO: Fix Grid Resetting on PlayMode. 
        //TODO: Make Checking Resolution actually do something.
        //TODO: Change private classes/variables to actually have a 'private' prefix
        
        #region Variables

            #region Serialized

            #if UNITY_EDITOR
            [BoxGroup("Serialized", showLabel: false)]  
            #endif
            [SerializeField] Vector2Int gridSize = new Vector2Int(100, 100);

            #if UNITY_EDITOR
            [BoxGroup("Serialized")]
            #endif
            [SerializeField] public float cellSize = 1;
        
            #if UNITY_EDITOR
            [BoxGroup("Serialized"), PropertySpace(SpaceBefore = 10)]
            #endif
            [SerializeField] public LayerMask layerMask;
        
            #if UNITY_EDITOR
            [BoxGroup("Serialized"), Range(0, 60)]
            #endif
            [SerializeField] float maxSlope = 30;

            #if UNITY_EDITOR
            [InlineButton("CheckResolutionUp", "+"), InlineButton("CheckResolutionDown", "-")]
            [BoxGroup("Serialized"), MinValue(1), MaxValue(5)]
            #endif
            [SerializeField] int checkResolution = 1;

            #if UNITY_EDITOR
            [BoxGroup("Serialized"), PropertySpace(SpaceBefore = 10)]
            #endif
            [SerializeField] public float skyLimit = 100;
        
            #if UNITY_EDITOR
            [ToggleLeft]
            [HorizontalGroup("Serialized/SeaLevel"), LabelText("Sea Level")]
            #endif
            [SerializeField] public bool useSeaLevel = true;
            #if UNITY_EDITOR
            [HorizontalGroup("Serialized/SeaLevel"), HideLabel, EnableIf("useSeaLevel")]
            #endif
            [SerializeField] public float seaLevel = -5f;

            #if UNITY_EDITOR
            [BoxGroup("Serialized"), AssetsOnly, PropertySpace(SpaceBefore = 10)]
            #endif
            [SerializeField] private GameObject cellPrefab;

            #region Editor Buttons

            #if UNITY_EDITOR

            [ButtonGroup("Serialized/GridGeneration")]
            [Button("Generate Grid", ButtonSizes.Medium), GUIColor(0.4f, 1f, 0.1f)]
            void GenerateGridButton() { GenerateGrid();}

            [ButtonGroup("Serialized/GridGeneration")]
            [Button("Clear Grid", ButtonSizes.Medium), GUIColor(1f, 0.4f, 0.1f)]
            void ClearGridButton() { ClearGrid();}

            void CheckResolutionUp()
            {
                checkResolution += 2;
            }
            void CheckResolutionDown() 
            {
                checkResolution -= 2;
            }
            
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

            [NonSerialized] public Cell[,] grid = new Cell[0,0];

            public Vector2Int GridWorldSize
            {
                get
                {
                    return new Vector2Int(Mathf.RoundToInt(gridSize.x * cellSize), Mathf.RoundToInt(gridSize.y * cellSize));
                }
            }

            [NonSerialized] public bool debugging = false, displayGizmos = true;
        
            //private static readonly Matrix4x4 left = new Matrix4x4(new Vector4(0f, 1f, 0f, 0f), new Vector4(-1f, 0f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
            //private static readonly Matrix4x4 right = new Matrix4x4(new Vector4(0f, -1f, 0f, 0f), new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
            //private static readonly Matrix4x4 flip = new Matrix4x4(new Vector4(-1f, 0f, 0f, 0f), new Vector4(0f, -1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));

            #endregion

        #endregion

        #region Methods

        private void Start()
        {
            GenerateGrid();
        }

        //private async Task GenerateGrid()
        void GenerateGrid()
        {
            DateTime startingTime = DateTime.Now;
            
            ClearGrid();
            
            SpawnGrid();
           
            Vector3 startingPosition = transform.position + new Vector3(-(((float)gridSize.x * cellSize) / 2f), 0, -(((float)gridSize.y * cellSize) / 2f));
            
            Vector3 cellPosition = new Vector3(0, skyLimit, 0);    
            
            //Parallel.For(0, gridSize.x, (x) =>
            for(int x = 0; x < gridSize.x; x++)
            {
                cellPosition.x = startingPosition.x + ((x * cellSize) + (cellSize/2f));
                
                for (int z = 0; z < gridSize.y; z++)
                {
                    cellPosition.z = startingPosition.z + ((z * cellSize) + (cellSize/2f));
                    
                    //if(debugging){Debug.Log(cellPosition.ToString());}
                    
                    RaycastHit hitData;
                    if (Physics.Raycast(cellPosition , -Vector3.up, out hitData, 
                        (useSeaLevel? (skyLimit - seaLevel) : Mathf.Infinity), layerMask))
                    {
                        float hitSlope = Vector3.Angle(Vector3.up, hitData.normal);
                        
                        if(debugging){Debug.Log(Convert.ToString(hitSlope));}

                        if (hitSlope <= maxSlope)
                        {
                            SpawnCell(new Vector2Int(x, z), Cell.AvailabilityState.Available, hitData.point);
                        }
                        else
                        {
                            SpawnCell(new Vector2Int(x, z), Cell.AvailabilityState.OutOfBounds, hitData.point);
                        }
                    }
                    else
                    {
                        SpawnCell(new Vector2Int(x, z), Cell.AvailabilityState.OutOfBounds, 
                            new Vector3(cellPosition.x, useSeaLevel? seaLevel : 0f, cellPosition.z));            
                    }
                }
            }
            
            TimeSpan totalTime = startingTime - DateTime.Now;
            
            if(debugging){Debug.Log(string.Format("Generating Grid took {0}", totalTime.ToString()));}
        }

        void ClearGrid()
        {
            if(debugging){Debug.Log("Clear Grid");}
            
            grid = new Cell[0,0];
            
            if (transform.childCount > 0)
            {
                if (Application.isEditor)
                {
                    DestroyImmediate(transform.GetChild(0).gameObject);
                }
                else
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }

        void SpawnGrid()
        {
            if(debugging){Debug.Log("Spawn Grid");}
            
            GameObject gridObject = new GameObject("Grid");

            gridObject.transform.parent = gameObject.transform;
            
            grid = new Cell[gridSize.x, gridSize.y];
        }

        void SpawnCell(Vector2Int index, Cell.AvailabilityState availability, Vector3 position)
        {
            GameObject spawnedCell = Instantiate(cellPrefab, position, Quaternion.identity, transform.GetChild(0));

            spawnedCell.transform.localScale = new Vector3(cellSize, 0, cellSize);
            
            CellObject cellObject = spawnedCell.GetComponent<CellObject>();
    
            if (cellObject != null)
            {
                cellObject.origin = this;
                cellObject.cell = null;
                //cellObject.index = index;
                
                grid[index.x,index.y] = new Cell(availability, cellObject);

                cellObject.cell = grid[index.x, index.y];

            }
            else
            {
                Debug.LogError("CellObject is null");
            }
        }
        
        #if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (displayGizmos)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(new Vector3(transform.position.x, (useSeaLevel? seaLevel : 0) , transform.position.z), 
                    new Vector3(gridSize.x * cellSize, 0, gridSize.y * cellSize)); 
                
                foreach (Cell cell in grid)
                {
                    switch (cell.Availability)
                    {
                        case Cell.AvailabilityState.Available:
                            Gizmos.color = Color.green;
                            break;
                        case Cell.AvailabilityState.Unavailable:
                            Gizmos.color = Color.red;
                            break;
                        case Cell.AvailabilityState.OutOfBounds:
                            Gizmos.color = Color.magenta;
                            break;
                    }
                    
                    Gizmos.DrawWireCube(cell.Position, new Vector3(cellSize, 0, cellSize)); 
                }
            }
        }
        #endif
        
        #endregion

    }
    
    public class Cell
    {
        /// <summary>
        /// Enum representing the state of the availability of this cell
        /// </summary>
        public enum AvailabilityState
        {
            /// <summary>
            /// This Cell is available.
            /// </summary>
            Available,

            /// <summary>
            /// This Cell is unavailable, for it is occupied.
            /// </summary>
            Unavailable,

            /// <summary>
            /// Nothing can be placed on this Cell.
            /// </summary>
            OutOfBounds
        }
        
        public AvailabilityState Availability { get; set; }
        
        public CellObject CellObject { get; private set; }
        
        public Vector3 Position
        {
            get
            {
                if (CellObject != null)
                {
                    return CellObject.transform.position;
                }
                else
                {
                    Debug.LogError("CellObject is null");
                    return Vector3.zero;
                }
            }
        }
        
        public Cell(AvailabilityState availability, CellObject cellObject)
        {
            Availability = availability;
            CellObject = cellObject;
        }
    }
}