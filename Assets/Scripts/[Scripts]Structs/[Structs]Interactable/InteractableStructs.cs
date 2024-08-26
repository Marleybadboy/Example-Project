using HCC.DataBase;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace HCC.Structs.Interactable 
{
    #region CuttableObject 

    [Serializable]
    public struct CuttableObject : IInteractable
    {
        #region Fields

        [BoxGroup("Cut Data"), HorizontalGroup("Cut Data/Cut Fields"), BoxGroup("Cut Data/Cut Fields/Cut")]
        [SerializeField] private CutData _cutData;

        [BoxGroup("Cut Data"), HorizontalGroup("Cut Data/Cut Fields"), BoxGroup("Cut Data/Cut Fields/Cut")]
        [SerializeField] private MeshFilter _cutMeshFilter;

        private bool _cutDisable;
        private int _currentHits;
        
        #endregion

        public void OnCollisionEnter(Collision collisionInfo)
        {
            Hit(collisionInfo);
        }

        public void OnCollisionExit(Collision collisionInfo){}

        public void OnTriggerEnter(Collider other){}

        public void OnTriggerExit(Collider other){}

        private void Hit(Collision collisionInfo) 
        {
            if (collisionInfo.gameObject.CompareTag(_cutData.InteractTag)) 
            {
                if (_cutDisable) return;
                
                _cutDisable = true;
                
                _currentHits++;

                if (_currentHits >= _cutData.MaxHits) 
                { 
                
                }

                new DOTimer(_cutData.HitDelay, EnableCut);
            }


        }

        private void EnableCut() 
        { 
            _cutDisable = false;
        }
    }

    #endregion
}