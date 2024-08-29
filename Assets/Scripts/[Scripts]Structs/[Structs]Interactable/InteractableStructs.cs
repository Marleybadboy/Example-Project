using EzySlice;
using HCC.DataBase;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using System;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace HCC.Structs.Interactable 
{
    #region CuttableObject 

    [Serializable]
    public class CuttableObject : IInteractable
    {
        #region Fields

        [Inject]
        private Inventory _inventory;

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
                bool inv = _inventory != null;

                Debug.Log("HITT" + _currentHits + " inventory is" + inv);

                if (_cutDisable) return;
                
                _cutDisable = true;
                
                _currentHits++;

                Debug.Log("HITT" + _currentHits);

                if (_currentHits >= _cutData.MaxHits) 
                {
                    ContactPoint point = collisionInfo.GetContact(0);

                    _contact = point.point;

                    CutObject();

                    return;
                }

                new DOTimer(_cutData.HitDelay, EnableCut);
            }


        }

        private void CutObject() 
        { 
            Cutter cutter = new Cutter(_cutMeshFilter,_cutData,_cutMeshFilter.transform.parent, _contact);

            cutter.Slice();

            MoveSlicedObject(cutter.UpperHull);

            _inventory.AddMulipleItem(_cutData.ItemValue, _cutData.ItemToAdd);

            UnityEngine.Object.Destroy(_cutMeshFilter.transform.parent.gameObject);
        }

        private void MoveSlicedObject(GameObject upperObject) 
        {
            if (upperObject.GetComponent<Rigidbody>() != null) return;

            Rigidbody rigd = upperObject.AddComponent<Rigidbody>();

            rigd.mass = _cutData.NewObjectMass;

            rigd.AddForceAtPosition(upperObject.transform.position, _cutData.ForcePush);
     
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

        public void AssignValue<T>(T value)
        {
            if (value is not Inventory inv) return;

            _inventory = inv;
        }

        #region Factory

        public class Factory : PlaceholderFactory<CuttableObject> 
        { 
            
        
        }

        #endregion
    }

    #endregion

    #region Cutter
    public struct Cutter 
    { 
        private MeshFilter _filter;
        private CutData _cutData;
        private Transform _parent;
        private float3 _contactPoint;

        private GameObject _upperHull;
        private GameObject _lowertHull;

        public delegate void OnCompleteSliceDelegete(GameObject upperPart, GameObject lowerPart);
        public OnCompleteSliceDelegete _onComplete;

        #region Properties
        public GameObject LowerHull { get => _lowertHull; }
        public GameObject UpperHull { get => _upperHull; }
        #endregion

        public Cutter(MeshFilter filter, CutData data, Transform parent, float3 contactPoint, OnCompleteSliceDelegete completeCallback) 
        { 
            _filter = filter;
            _cutData = data;
            _parent = parent;
            _contactPoint = contactPoint;
            _onComplete = completeCallback;

            _upperHull = null;
            _lowertHull = null;
        }

        public Cutter(MeshFilter filter, CutData data, Transform parent, float3 contactPoint)
        {
            _filter = filter;
            _cutData = data;
            _parent = parent;
            _contactPoint = contactPoint;
            _onComplete = null;

            _upperHull = null;
            _lowertHull = null;
        }

        public void Slice() 
        {
            SlicedHull hull = _filter.gameObject.Slice(_contactPoint, Vector3.up);

            if (hull == null) return;

            GameObject upperhull = hull.CreateUpperHull(_filter.gameObject,_cutData.AfterCutMaterial);
            GameObject lowerHull = hull.CreateLowerHull(_filter.gameObject, _cutData.AfterCutMaterial);

            upperhull.transform.position = _parent.position;
            lowerHull.transform.position = _parent.position;

            upperhull.layer = LayerMask.NameToLayer("Not Interactable");
            lowerHull.layer = LayerMask.NameToLayer("Terrain");

            var upCollider = upperhull.AddComponent<CapsuleCollider>();
            var downCollider = lowerHull.AddComponent<BoxCollider>();

            var obCollider = _filter.gameObject.GetComponent<CapsuleCollider>();

            upCollider.radius = obCollider.radius;
            upCollider.center = new float3(0.05f, 5f, -0.04f);
           

            _upperHull = upperhull;
            _lowertHull = lowerHull;

            if (_onComplete == null) return;

            _onComplete(upperhull, lowerHull);

        }




    }
    #endregion
}