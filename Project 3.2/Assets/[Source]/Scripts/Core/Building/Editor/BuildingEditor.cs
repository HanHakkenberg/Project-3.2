#if UNITY_EDITOR
using Sirenix.OdinInspector;
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

namespace Core.Building
{   
    public class BuildingEditor : OdinMenuEditorWindow
    {
        
        #region Settings
        
        [SerializeField] private BuildingEditorSettings settings = new BuildingEditorSettings();
        
        #endregion
        
        [MenuItem("Walter/Building Editor")]
        private static void Open()
        {
            BuildingEditor window = GetWindow<BuildingEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false);
            tree.DefaultMenuStyle.IconSize = 26;
            tree.Config.DrawSearchToolbar = true;

            OdinMenuItem settingsMenu = new OdinMenuItem(tree, "Settings", this.settings);
            settingsMenu.Icon = EditorIcons.SettingsCog.Raw;
            tree.MenuItems.Insert(0, settingsMenu);
            
            tree.AddAllAssetsAtPath("Constructions", settings.resourcePath, typeof(Construction), true);
            tree.EnumerateTree().AddIcons<Construction>(x => x.Icon);

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            int toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (SirenixEditorGUI.ToolbarButton(EditorIcons.Plus))
                {
                    ScriptableObjectCreator.ShowDialog<Construction>(settings.resourcePath, obj =>
                    {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj);
                    });
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }

    [HideLabel]
    [Serializable]
    public class BuildingEditorSettings
    {
        [FolderPath] //Assets/[Source]/Prefabs/Resources
        public string resourcePath = "Assets/[Source]/Prefabs/Constructions";
    }
    
    public static class ScriptableObjectCreator
    {
        public static void ShowDialog<T>(string defaultDestinationPath, Action<T> onScritpableObjectCreated = null)
            where T : ScriptableObject
        {
            var selector = new ScriptableObjectSelector<T>(defaultDestinationPath, onScritpableObjectCreated);

            if (selector.SelectionTree.EnumerateTree().Count() == 1)
            {
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
            private Action<T> onScriptableObjectCreated;
            private string defaultDestinationPath;

            public ScriptableObjectSelector(string defaultDestinationPath, Action<T> onScriptableObjectCreated = null)
            {
                this.onScriptableObjectCreated = onScriptableObjectCreated;
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

                    if (this.onScriptableObjectCreated != null)
                    {
                        this.onScriptableObjectCreated(obj);
                    }
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(obj);
                }
            }
        }
    }
}