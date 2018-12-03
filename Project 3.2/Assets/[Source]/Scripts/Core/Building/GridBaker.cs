using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

namespace Core.Building
{
    [AddComponentMenu("Core/Building/GridBaker")]
    public class GridBaker : SerializedMonoBehaviour
    {
        //TODO: FIx cellScale wrongly affecting cell position.
        //TODO: Remove 1,1 extra offset bug.
        //TODO: Change private classes/variables to actually have a 'private' prefix
        
        #region Variables

            #region Serialized

            [BoxGroup("Serialized", showLabel: false)]       
            [SerializeField] Vector2Int gridSize = new Vector2Int(100, 100);

            [BoxGroup("Serialized")]
            [SerializeField] public float cellSize = 1;
            
            [BoxGroup("Serialized"), PropertySpace(SpaceBefore = 10)]
            [SerializeField] public LayerMask layerMask;
        
            [BoxGroup("Serialized"), Range(0, 60)]
            [SerializeField] float maxSlope = 30;
            [BoxGroup("Serialized"), MinValue(1), MaxValue(5)]
            [SerializeField] int checkResolution = 1;

            [BoxGroup("Serialized"), PropertySpace(SpaceBefore = 10)]
            [SerializeField] public float skyLimit = 100;
        
            [ToggleLeft]
            [HorizontalGroup("Serialized/SeaLevel"), LabelText("Sea Level")]
            [SerializeField] public bool useSeaLevel = true;
            [HorizontalGroup("Serialized/SeaLevel"), HideLabel, EnableIf("useSeaLevel")]
            [SerializeField] public float seaLevel = -5f;

            [BoxGroup("Serialized"), AssetsOnly, PropertySpace(SpaceBefore = 10)]
            [SerializeField] private GameObject cellPrefab;

            #region Editor Buttons

            #if UNITY_EDITOR

            [HorizontalGroup("Serialized/GridGeneration")]
            [Button("Generate Grid", ButtonSizes.Medium)]
            void GenerateGridButton() { GenerateGrid();}
            //async void GenerateGridButton() { await GenerateGrid();}
            [HorizontalGroup("Serialized/GridGeneration")]
            [Button("Clear Grid", ButtonSizes.Medium)]
            void ClearGridButton() { ClearGrid();}
            
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

            //[TableMatrix(SquareCells = true)]
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
        //private async Task GenerateGrid()
        void GenerateGrid()
        {
            DateTime startingTime = DateTime.Now;
            
            ClearGrid();
            
            SpawnGrid();
            
            grid = new Cell[gridSize.x, gridSize.y];

            Vector3 startingPosition = transform.position + new Vector3(-(((float)gridSize.x * cellSize) / 2f), 0, -(((float)gridSize.y * cellSize) / 2f));
            
            Vector3 cellPosition = new Vector3(0, skyLimit, 0);            
            
            //Parallel.For(0, gridSize.x, (x) =>
            for(int x = 0; x < gridSize.x; x++)
            {
                cellPosition.x = startingPosition.x + (((x + 1) * cellSize) + (cellSize/2f));
                //cellPosition.x = (-gridSize.x /2f) + ()
                
                for (int z = 0; z < gridSize.y; z++)
                {
                    cellPosition.z = startingPosition.z + (((z + 1) * cellSize) + (cellSize/2f));
                    
                    if(debugging){Debug.Log(cellPosition.ToString());}
                    
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
            grid = new Cell[0,0];
            
            if (transform.childCount > 0)
            {
                if (Application.isEditor)
                {
                    //DestroyImmediate(transform.GetChild(0));
                    DestroyImmediate(transform.GetChild(0).gameObject);
                }
                else
                {
                    //Destroy(transform.GetChild(0));
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }

        void SpawnGrid()
        {
            GameObject childObject = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, transform) as GameObject;

            //childObject.transform.parent = transform;

            childObject.name = "Grid";   
        }

        void SpawnCell(Vector2Int index, Cell.AvailabilityState availability, Vector3 position)
        {
            GameObject spawnedCell = Instantiate(cellPrefab, position, Quaternion.identity, transform.GetChild(0));
    
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
                    new Vector3(gridSize.x, 0, gridSize.y)); 
                
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
            /// Nothing can be placed on this Cell,
            /// </summary>
            OutOfBounds
        }
        
        public AvailabilityState Availability { get; private set; }
        
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

        /*
        public Vector3 Position { get; private set; }
        
        public Cell(AvailabilityState availability, Vector3 position)
        {
            Availability = availability;
            Position = position;
        }
        */
    }

    [CustomEditor(typeof(GridBaker), editorForChildClasses: false)]
    public class GridBakerEditor : OdinEditor
    {        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        void OnSceneGUI()
        {
            if (TargetGridBaker != null)
            {
                Input();
                
                if (TargetGridBaker.displayGizmos) { Draw(); }
            }
            else
            {
                Debug.LogError("GridBaker has not been assigned");
            }
        }

        private GridBaker TargetGridBaker
        {
            get
            {
                if (target != null)
                {
                    if (target.GetType() != typeof(GridBaker))
                    {
                        Debug.LogError(string.Format("GridBakerEditor is not a custom editor of type GridBaker, it's of type {0}", target.GetType()));
                    }
                    else
                    {
                        return (GridBaker) (target as GridBaker);
                    }
                }
                
                Debug.LogError("Custom Editor does not have a target");

                return null;
            }
        }

        void Input()
        {
            Event guiEvent = Event.current;
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        }

        void Draw()
        {
            GridBaker gridBaker = TargetGridBaker;
            
            Vector3 gridPos = gridBaker.transform.position;
            
            
            //DebugExtension.DebugBounds(new Bounds
            //(
            //    new Vector3(gridPos.x, (gridBaker.useSeaLevel? gridBaker.seaLevel : gridPos.y), gridPos.z),
            //    new Vector3(gridBaker.GridWorldSize.x, 0, gridBaker.GridWorldSize.y
            //)), Color.cyan);
            
            Cell[,] grid = gridBaker.grid;
            
            if (grid != null)
            {
                foreach (Cell cell in grid)
                {                    
                    //Gizmos.color = (cell.Occupied) ? Color.green : Color.red;
                    //DebugExtension.DebugLocalCube(cell.Position, new Vector3(gridBaker.cellSize, 0, gridBaker.cellSize));
                    
                    //DebugExtension.DebugLocalCube(gridBaker.transform, new Vector3(gridBaker.cellSize, 0, gridBaker.cellSize), (cell.Occupied) ? Color.green : Color.red);

                    EditorGUI.BeginChangeCheck();
                    
                    //Vector3 newPosition = Handles.FreeMoveHandle(cell.Position, Quaternion.identity, 1, Vector3.one * 0.1f, Handles.DotHandleCap);
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Move Cell");
                        //cell.Position = newPosition;
                    }
                }
            }
            else{ Debug.LogError("GridBaker does not have a grid assigned");}
        }
    }
}