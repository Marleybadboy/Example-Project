using HCC.DataBase;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HCC.Interactable
{
    public class MiningObject : IInteractable
    {
        #region Fields
        [BoxGroup("Mining Data")]
        [SerializeField] private MiningData _mineData;


        [Inject]
        private Inventory _inventory;
        private bool _minDisable;

        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public void OnCollisionEnter(Collision collisionInfo)
        {
            Hit(collisionInfo);
        }

        private void Hit(Collision collisionInfo)
        {
            if (HitByPickaxeObject(collisionInfo))
            {
                bool inv = _inventory != null;

                Debug.Log("HITT" +" inventory is" + inv);

                if (_minDisable) return;

                _minDisable = true;

                _inventory.AddMulipleItem(_mineData.ItemValue, _mineData.ItemToAdd);

                new DOTimer(_mineData.HitDelay, EnableCut);
            }
        }

        private void EnableCut()
        {
           _minDisable = false;
        }

        public void OnCollisionExit(Collision collisionInfo)
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            
        }

        public void OnTriggerExit(Collider other)
        {
            
        }

        private bool HitByPickaxeObject(Collision collisionInfo)
        {
            collisionInfo.gameObject.TryGetComponent(out ItemMonoComponent component);

            if (component == null) return false;

            if (component.ItemType is not UsefullItem) return false;

            if (component.ItemType.Identyfier.ItemIdentyfication != _mineData.InteractTag) return false;

            return true;
        }

        #endregion

    }
}
