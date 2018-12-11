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

//#if UNITY_EDITOR

namespace Core.Building
{
    [Serializable]
    public class ResourceList
    {
        #if UNITY_EDITOR
        [ValueDropdown("CustomAddResourceButton", IsUniqueList = true, DrawDropdownForListElements = false,
            DropdownTitle = "Modify Stats")]
        [ListDrawerSettings(DraggableItems = false, Expanded = true)]
        #endif

        [SerializeField]
        private List<ResourceValue> stats = new List<ResourceValue>();

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
    #endif

    [Serializable]
    public struct ResourceValue : IEquatable<ResourceValue>
    {
        [HideInInspector] public CivManager.Type ResourceType;

        #if UNITY_EDITOR
        [Range(-100, 100)] [LabelWidth(103)] [LabelText("$ResourceType")]
        #endif
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
}