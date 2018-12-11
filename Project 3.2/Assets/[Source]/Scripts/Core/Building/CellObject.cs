using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Building
{
    [RequireComponent(typeof(BoxCollider))]
    public class CellObject : MonoBehaviour
    {
        #region Variables
        
//        [SerializeField] public BoxCollider boxCollider;
//
//        [NonSerialized] public GridBaker origin;
//        
//        [NonSerialized] public Cell cell;
        
        
            public BoxCollider boxCollider;

            public GridBaker origin;
        
            public Cell cell;
        
        #endregion

        #region Methods
        
        private void OnEnable()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        
        #endregion
    }
}
