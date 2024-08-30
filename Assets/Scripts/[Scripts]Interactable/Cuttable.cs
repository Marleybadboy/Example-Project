using HCC.DataBase;
using HCC.Interfaces;
using HCC.Structs.Interactable;
using Sirenix.OdinInspector;
using System;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace HCC.Interactable
{
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

        public void OnCollisionExit(Collision collisionInfo) { }

        public void OnTriggerEnter(Collider other) { }

        public void OnTriggerExit(Collider other) { }

        private void Hit(Collision collisionInfo)
        {
            if (HitByCutObject(collisionInfo))
            {
                bool inv = _inventory != null;
                
                if (_cutDisable) return;

                _cutDisable = true;

                _currentHits++;

                StaticEvents.StaticEvents.ExecuteShowMessage("Tree hit!");

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
            Cutter cutter = new Cutter(_cutMeshFilter, _cutData, _cutMeshFilter.transform.parent, _contact);

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
    }
}
