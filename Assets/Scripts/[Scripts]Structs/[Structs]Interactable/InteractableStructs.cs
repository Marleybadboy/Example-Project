using EzySlice;
using HCC.DataBase;
using Unity.Mathematics;
using UnityEngine;

namespace HCC.Structs.Interactable 
{
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