using HCC.DataBase;
using HCC.GUI.InventoryUI;
using HCC.Interfaces;
using HCC.Structs.GUI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace HCC.GUI
{
    public class InventoryManager : GUIManager
    {
        #region Fields

        private GUIInventoryData _inventoryData;
        private List<ItemSlot> _itemSlotsData;

        [Inject]
        private Inventory _inventory;

        [Inject]
        private CraftingManager _craftingManager;

        #endregion

        #region Properties
        #endregion

        #region Functions

        #endregion

        #region Methods
        public void AssignInvontoryToCrafting() 
        {
            _craftingManager.AssignInventory(this);
        }

        public override void SetData(IUIDataHolder holder)
        {
            _inventoryData = (GUIInventoryData)holder;
        }

        #endregion
        public override void CloseUI()
        {
            _inventoryData.ActiveInventory = false;

            ResetSlots();

            _craftingManager.CloseUI();
        }

        public override void OpenUI()
        {
            _inventoryData.ActiveInventory = true;

            ResetSlots();

            SpawnInventoryObjects();

            _craftingManager.OpenUI();
        }

        private void SpawnInventoryObjects() 
        {

            if (_inventory.Items == null) return;

            if (_inventory.Items.Count <= 0) return;

            _itemSlotsData = _inventoryData.CreateSlots(_inventory.Items, this).ToList();
        }

        public void CreateSingleInventoryObject(Recipe recipe) 
        { 
            List<ItemSlot> newItem = _inventoryData.CreateSlots(new List<Item> { recipe.CreatedItem }, this).ToList();

            List<ItemSlot> updateList = _itemSlotsData.ToList();

            for(int i  = 0; i < newItem.Count; i++) 
            {
                updateList.Add(newItem[i]);

                _inventory.AddItem(newItem[i].ItemInSlot);
            }

            _itemSlotsData = updateList; 

            for(int i = 0;i < recipe.ItemsToCraft.Length; i++) 
            {
                _inventory.RemoveItem(recipe.ItemsToCraft[i]);
            }
        }

        public void RemoveItemSlot(ItemSlot slot) 
        { 
            if(_itemSlotsData == null) return;

            if (_itemSlotsData.Contains(slot)) 
            { 
                _itemSlotsData.Remove(slot);

                Debug.Log("removed" + _itemSlotsData.Count);
            }
        }

        public void DropItem(Item item) 
        { 
            _inventory.Drop(item);
        }

        private void ResetSlots() 
        {
            if (_itemSlotsData == null) return;

            if (_itemSlotsData.Count <= 0) return;

            for(int i = 0;  i < _itemSlotsData.Count; i++) 
            {
                Object.Destroy(_itemSlotsData[i].gameObject);
            }

            _itemSlotsData.Clear();


        }
    }

}
