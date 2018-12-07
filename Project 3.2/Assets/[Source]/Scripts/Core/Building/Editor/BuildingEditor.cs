using Sirenix.OdinInspector;

namespace Core.Building
{
    #if UNITY_EDITOR
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using System.Collections;
    
    using UnityEditor;
    #endif
    
    using UnityEngine;
    using System.Linq;
    
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    using ObjectFieldAlignment = Sirenix.OdinInspector.ObjectFieldAlignment;

    #if UNITY_EDITOR
    
    public class BuildingEditor : OdinMenuEditorWindow
    {
        
        #region Settings
        
        //[ContextMenu("Editor Settings")]
        [MenuItem("Building Editor/Settings", false, 0)]
        void OpenEditorSettings()
        {
            var window = UnityEditor.EditorWindow.GetWindow<BuildingEditorSettings>();
            window.WindowPadding = new Vector4(); 
        }
        
        #endregion
        
        [MenuItem("Walter/Building Editor")]
        private static void Open()
        {
            var window = GetWindow<BuildingEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath("Constructions", BuildingEditorSettings.resourcePath, typeof(Construction), true);

            tree.EnumerateTree().AddIcons<Building>(x => x.Icon);

            return tree;
        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }
        
        
        /*
        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
                {
                    ScriptableObjectCreator.ShowDialog<Item>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Items", obj =>
                    {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Character")))
                {
                    ScriptableObjectCreator.ShowDialog<Character>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Character", obj =>
                    {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
         */
    }

    public class BuildingEditorSettings : OdinEditorWindow
    {
        [FolderPath(ParentFolder = "Assets/Resources")]
        public static string resourcePath = "Assets/[Source]/Resources";
    }
   
    public static class ScriptableObjectCreator
    {
        public static void ShowDialog<T>(string defaultDestinationPath, Action<T> onScritpableObjectCreated = null)
            where T : ScriptableObject
        {
            var selector = new ScriptableObjectSelector<T>(defaultDestinationPath, onScritpableObjectCreated);

            if (selector.SelectionTree.EnumerateTree().Count() == 1)
            {
                // If there is only one scriptable object to choose from in the selector, then 
                // we'll automatically select it and confirm the selection. 
                selector.SelectionTree.EnumerateTree().First().Select();
                selector.SelectionTree.Selection.ConfirmSelection();
            }
            else
            {
                // Else, we'll open up the selector in a popup and let the user choose.
                selector.ShowInPopup(200);
            }
        }

        private class ScriptableObjectSelector<T> : OdinSelector<Type> where T : ScriptableObject
        {
            private Action<T> onScritpableObjectCreated;
            private string defaultDestinationPath;

            public ScriptableObjectSelector(string defaultDestinationPath, Action<T> onScritpableObjectCreated = null)
            {
                this.onScritpableObjectCreated = onScritpableObjectCreated;
                this.defaultDestinationPath = defaultDestinationPath;
                this.SelectionConfirmed += this.ShowSaveFileDialog;
            }

            protected override void BuildSelectionTree(OdinMenuTree tree)
            {
                var scriptableObjectTypes = AssemblyUtilities.GetTypes(AssemblyTypeFlags.CustomTypes)
                    .Where(x => x.IsClass && !x.IsAbstract && x.InheritsFrom(typeof(T)));

                tree.Selection.SupportsMultiSelect = false;
                tree.Config.DrawSearchToolbar = true;
                tree.AddRange(scriptableObjectTypes, x => x.GetNiceName())
                    .AddThumbnailIcons();
            }

            private void ShowSaveFileDialog(IEnumerable<Type> selection)
            {
                var obj = ScriptableObject.CreateInstance(selection.FirstOrDefault()) as T;

                string dest = this.defaultDestinationPath.TrimEnd('/');

                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                    AssetDatabase.Refresh();
                }

                dest = EditorUtility.SaveFilePanel("Save object as", dest, "New " + typeof(T).GetNiceName(), "asset");

                if (!string.IsNullOrEmpty(dest) && PathUtilities.TryMakeRelative(Path.GetDirectoryName(Application.dataPath), dest, out dest))
                {
                    AssetDatabase.CreateAsset(obj, dest);
                    AssetDatabase.Refresh();

                    if (this.onScritpableObjectCreated != null)
                    {
                        this.onScritpableObjectCreated(obj);
                    }
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(obj);
                }
            }
        }
    }
    
    #endif
    
    public class Construction : SerializedScriptableObject
    {
        protected const string LEFT             = "Alignment/Left";
        protected const string RIGHT            = "Alignment/Right";
        
        //protected const string SETTINGS         = "Main/" + LEFT + "/Settings";
        //protected const string DESCRIPTION      = "Main/" + RIGHT + "/Description";
        
        protected const string SETTINGS         = LEFT + "/Settings";
        protected const string DESCRIPTION      = RIGHT + "/Description";
        
        protected const string SETTINGS_LEFT    = SETTINGS + "/" + LEFT;
        protected const string SETTINGS_RIGHT   = SETTINGS + "/" + RIGHT;
       
        
        #region Main
        
        //[BoxGroup("Main", false)]
        //[HorizontalGroup("Main/Alignment")]
        #if UNITY_EDITOR
        [HorizontalGroup("Alignment")]

        [VerticalGroup(LEFT)]
        
        [BoxGroup(SETTINGS)]
        [HorizontalGroup(SETTINGS + "/Alignment", 64, LabelWidth = 48)]
        //[HorizontalGroup(SETTINGS + "/Alignment", 0.5f, LabelWidth = 48)]
        
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
        [VerticalGroup(RIGHT)]

        [BoxGroup(DESCRIPTION)]
        [HideLabel, MultiLineProperty(4)]
        #endif
        public string Description;
        
        #endregion

        #region Additional

        #if UNITY_EDITOR
        [BoxGroup("Additional", false)]
        [HorizontalGroup("Additional/Alignment", 0.5f)]
        //[HorizontalGroup("Additional/Alignment", 64, LabelWidth = 48)]
        
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
        [TableMatrix(DrawElementMethod = "DrawColoredCell", SquareCells = true, HideRowIndices = true, HideColumnIndices = true)]
        #endif    
        public bool[,] FootPrint = new bool[25, 25];
        
        #if UNITY_EDITOR

        private static bool DrawColoredCell(Rect rect, bool toggled)
        {
            Event currentEvent = Event.current;
            
            if (currentEvent.type == EventType.MouseDown && (currentEvent.button == 0 && currentEvent.isMouse) && rect.Contains(Event.current.mousePosition))
            {
                toggled = !toggled;
                GUI.changed = true;
                Event.current.Use();
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(1), toggled ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));

            return toggled;
        }

        #endif
        
        //[TabGroup("Footprint")]
        //public Sirenix.OdinInspector.Demos.RPGEditor.ItemSlot[,] Inventory = new Sirenix.OdinInspector.Demos.RPGEditor.ItemSlot[25, 25];

        #endregion
    }

    public class Building : Construction
    {
        
    }

    [Serializable]
    public class ResourceList
    {
        #if UNITY_EDITOR
        [ValueDropdown("CustomAddResourceButton", IsUniqueList = true, DrawDropdownForListElements = false, DropdownTitle = "Modify Stats")]
        [ListDrawerSettings(DraggableItems = false, Expanded = true)]
        #endif
        [SerializeField] private List<ResourceValue> stats = new List<ResourceValue>();

        public ResourceValue this[int index]
        {
            get { return this.stats[index]; }
            set { this.stats[index] = value; }
        }

        public int Count
        {
            get { return this.stats.Count; }
        }

        public float this[CivManager.Type type]
        {
            get
            {
                for (int i = 0; i < this.stats.Count; i++)
                {
                    if (this.stats[i].ResourceType == type)
                    {
                        return this.stats[i].Value;
                    }
                }

                return 0;
            }
            set
            {
                for (int i = 0; i < this.stats.Count; i++)
                {
                    if (this.stats[i].ResourceType == type)
                    {
                        var val = this.stats[i];
                        val.Value = value;
                        this.stats[i] = val;
                        return;
                    }
                }

                this.stats.Add(new ResourceValue(type, value));
            }
        }

        #if UNITY_EDITOR
        
        // Finds all available ResourceTypes and excludes the types that the list already contains, so we don't get multiple entries of the same type.
        private IEnumerable CustomAddResourceButton()
        {
            return Enum.GetValues(typeof(CivManager.Type)).Cast<CivManager.Type>()
                .Except(this.stats.Select(t => t.ResourceType))
                .Select(v => new ResourceValue(v))
                .AppendWith(this.stats)
                .Select(x => new ValueDropdownItem(x.ResourceType.ToString(), x));
        }
        
        #endif
    }

    #if UNITY_EDITOR

    internal class StatListValueDrawer : OdinValueDrawer<ResourceList>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // This would be the "private List<StatValue> stats" field.
            this.Property.Children[0].Draw(label);
        }
    }

    [Serializable]
    public struct ResourceValue : IEquatable<ResourceValue>
    {
        [HideInInspector]
        public CivManager.Type ResourceType;

        [Range(-100, 100)]
        [LabelWidth(103)]
        [LabelText("$ResourceType")]
        public float Value;

        public ResourceValue(CivManager.Type type, float value)
        {
            this.ResourceType = type;
            this.Value = value;
        }

        public ResourceValue(CivManager.Type type)
        {
            this.ResourceType = type;
            this.Value = 0;
        }

        public bool Equals(ResourceValue other)
        {
            return this.ResourceType == other.ResourceType && this.Value == other.Value;
        }
    }
    
    #endif
}