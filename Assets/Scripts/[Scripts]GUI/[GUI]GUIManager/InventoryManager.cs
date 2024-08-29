using HCC.GUI.InventoryUI;
using HCC.Interfaces;
using HCC.Structs.GUI;
using UnityEngine;
using Zenject;

namespace HCC.GUI
{
    public class InventoryManager : GUIManager
    {
        #region Fields

        private GUIInventoryData _inventoryData;
        private ItemSlot[] _itemSlotsData;

        [Inject]
        private Inventory _inventory;

        #endregion

        #region Properties
        #endregion

        #region Functions

        #endregion

        #region Methods
        public override void SetData(IUIDataHolder holder)
        {
            _inventoryData = (GUIInventoryData)holder;
        }

        #endregion
        public override void CloseUI()
        {
            _inventoryData.ActiveInventory = false;

            ResetSlots();
        }

        public override void OpenUI()
        {
            _inventoryData.ActiveInventory = true;

            ResetSlots();

            SpawnInventoryObjects();
        }

        private void SpawnInventoryObjects() 
        {





            Debug.LogError($"KURWA JEST CZY NIE: {_inventory != null}");

            if (_inventory.Items == null) return;

            if (_inventory.Items.Count <= 0) return;

            _itemSlotsData = _inventoryData.CreateSlots(_inventory.Items);
        }

        private void ResetSlots() 
        {
            if (_itemSlotsData == null) return;

            if (_itemSlotsData.Length <= 0) return;

            for(int i = 0;  i < _itemSlotsData.Length; i++) 
            {
                Object.Destroy(_itemSlotsData[i].gameObject);
            }
        
        }
    }
}
