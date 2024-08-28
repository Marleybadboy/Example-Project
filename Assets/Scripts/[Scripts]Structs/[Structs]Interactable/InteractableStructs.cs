using EzySlice;
using HCC.DataBase;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using System;
using Unity.Mathematics;
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
        private float3 _contact;
        
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
            if (HitByCutObject(collisionInfo)) 
            {
                Debug.Log("HITT");


                if (_cutDisable) return;
                
                _cutDisable = true;
                
                _currentHits++;

                if (_currentHits >= _cutData.MaxHits) 
                {
                    ContactPoint point = collisionInfo.GetContact(0);

                    _contact = point.point;

                    CutObject();

                    UnityEngine.Object.Destroy(_cutMeshFilter.transform.parent.gameObject);

                    return;
                }

                new DOTimer(_cutData.HitDelay, EnableCut);
            }


        }

        private void CutObject() 
        { 
            Cutter cutter = new Cutter(_cutMeshFilter,_cutData,_cutMeshFilter.transform.parent, _contact);

            cutter.Slice();
        
        }

        private bool HitByCutObject(Collision collisionInfo) 
        {
            collisionInfo.gameObject.TryGetComponent(out ItemMonoComponent component);

            if (component == null) return false;

            if (component.ItemType is not UsefullItem) return false;

            if (component.ItemType.Identyfier.ItemIdentyfication != _cutData.InteractTag) return false;

            return true;
        }

        private void EnableCut() 
        { 
            _cutDisable = false;
        }
    }

    #endregion

    #region Cutter
    public struct Cutter 
    { 
        private MeshFilter _filter;
        private CutData _cutData;
        private Transform _parent;
        private float3 _contactPoint;

        public delegate void OnCompleteSliceDelegete(GameObject upperPart, GameObject lowerPart);
        public OnCompleteSliceDelegete _onComplete;

        public Cutter(MeshFilter filter, CutData data, Transform parent, float3 contactPoint, OnCompleteSliceDelegete completeCallback) 
        { 
            _filter = filter;
            _cutData = data;
            _parent = parent;
            _contactPoint = contactPoint;
            _onComplete = completeCallback;
        }

        public Cutter(MeshFilter filter, CutData data, Transform parent, float3 contactPoint)
        {
            _filter = filter;
            _cutData = data;
            _parent = parent;
            _contactPoint = contactPoint;
            _onComplete = null;
        }

        public void Slice() 
        {
            SlicedHull hull = _filter.gameObject.Slice(_contactPoint, Vector3.up);

            if (hull == null) return;

            GameObject upperhull = hull.CreateUpperHull(_filter.gameObject,_cutData.AfterCutMaterial);
            GameObject lowerHull = hull.CreateLowerHull(_filter.gameObject, _cutData.AfterCutMaterial);

            upperhull.transform.position = _parent.position;
            lowerHull.transform.position = _parent.position;

            var upCollider = upperhull.AddComponent<CapsuleCollider>();
            var obCollider = _filter.gameObject.GetComponent<CapsuleCollider>();

            upCollider.radius = obCollider.radius;

            if (_onComplete == null) return;

            _onComplete(upperhull, lowerHull);

        }




    }
    #endregion
}