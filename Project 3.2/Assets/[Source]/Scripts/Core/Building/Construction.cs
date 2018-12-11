using UnityEngine;
//#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.Utilities;
//#endif

namespace Core.Building
{
    public class Construction : SerializedScriptableObject
    {
        protected const string LEFT = "Alignment/Left";
        protected const string RIGHT = "Alignment/Right";

        //protected const string SETTINGS         = "Main/" + LEFT + "/Settings";
        //protected const string DESCRIPTION      = "Main/" + RIGHT + "/Description";

        protected const string SETTINGS = LEFT + "/Settings";
        protected const string DESCRIPTION = RIGHT + "/Description";

        protected const string SETTINGS_LEFT = SETTINGS + "/" + LEFT;
        protected const string SETTINGS_RIGHT = SETTINGS + "/" + RIGHT;


        #region Main

        //[BoxGroup("Main", false)]
        //[HorizontalGroup("Main/Alignment")]
        #if UNITY_EDITOR
        [HorizontalGroup("Alignment")]
        [VerticalGroup(LEFT)]
        [BoxGroup(SETTINGS)]
        [HorizontalGroup(SETTINGS + "/Alignment", 64, LabelWidth = 48)]

        [VerticalGroup(SETTINGS_LEFT)]
        [HideLabel, PreviewField(64)]
        #endif
        public Texture Icon;

        #if UNITY_EDITOR
        [VerticalGroup(SETTINGS_RIGHT)]
        #endif
        public string Name;

        #if UNITY_EDITOR
        [VerticalGroup(SETTINGS_RIGHT), AssetsOnly]
        #endif
        public GameObject Prefab;

        #if UNITY_EDITOR
        [VerticalGroup(RIGHT)] [BoxGroup(DESCRIPTION)] [HideLabel, MultiLineProperty(4)]
        #endif
        public string Description;

        #endregion

        #region Additional

        #if UNITY_EDITOR
        [BoxGroup("Additional", false)]
        [HorizontalGroup("Additional/Alignment", 0.5f)]

        [VerticalGroup("Additional/" + LEFT)]
        #endif
        public ResourceList ResourceModifiers;

        #if UNITY_EDITOR
        [VerticalGroup("Additional/" + RIGHT)]
        #endif
        public ResourceList LimitModifiers;

        #endregion

        #region FootPrint

        #if UNITY_EDITOR
        [BoxGroup("Footprint")]
        [TableMatrix(DrawElementMethod = "DrawColoredCell", SquareCells = true, HideRowIndices = false,
            HideColumnIndices = false, ResizableColumns = false)]
        #endif
        public bool[,] footPrint = new bool[25, 25]
        {
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, true, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            },
            {
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
            }
        };

        #if UNITY_EDITOR

        private static bool DrawColoredCell(Rect rect, bool toggled)
        {
            Event currentEvent = Event.current;

            if (currentEvent.type == EventType.MouseDown && (currentEvent.button == 0 && currentEvent.isMouse) &&
                rect.Contains(Event.current.mousePosition))
            {
                toggled = !toggled;
                GUI.changed = true;
                Event.current.Use();
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(1), toggled ? Color.green : Color.grey);

            return toggled;
        }

        #endif

        #endregion
    }
    
    public class Building : Construction
    {
        
    }
}