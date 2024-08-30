
using DG.Tweening;
using HCC.DataBase;
using HCC.Interfaces;
using HCC.Structs.GUI;
using UnityEngine;

namespace HCC.GUI
{
    public class CraftingManager : GUIManager
    {
        #region Fields
        private CraftingData _craftingData;
        private InventoryManager _inventoryManager;
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public void AssignInventory(InventoryManager inventory) 
        {
            _inventoryManager = inventory;
        }

        public override void CloseUI()
        {
            _craftingData.ResetSlots();

            _craftingData.ResetCallback();

            _craftingData.ActiveGroup = 0f;
        }

        public override void OpenUI()
        {
            _craftingData.ResetSlots();

            _craftingData.AssignCraftingButton(StartCraft);

            _craftingData.ActiveGroup = 1f;
        }

        public override void SetData(IUIDataHolder holder)
        {
            _craftingData = (CraftingData)holder;

            _craftingData.CreateDataForRecipes();
        }

        private void StartCraft() 
        {
            _craftingData.ExecuteCraftingTween().OnComplete(() => { Craft(); _craftingData.ResetSlider(); });
        }

        public void Craft()
        {
            bool canCraft = _craftingData.FindRecipe(out Recipe recipe);

            if (!canCraft) { StaticEvents.StaticEvents.ExecuteShowMessage("Is no recipe to craft!"); _craftingData.ResetSlots(); _craftingData.ResetSlider(); }

            float randomValue = Random.Range(0f, 1f);

            if (randomValue < recipe.Probality)
            {
                StaticEvents.StaticEvents.ExecuteItemCollected($"Congrats! Item created: {recipe.CreatedItem.Identyfier.ItemIdentyfication}");

                _craftingData.ResetSlots();

                _inventoryManager.CreateSingleInventoryObject(recipe);

                return;
            }

            FailureCrafting();

        }

        private void FailureCrafting()
        {
            StaticEvents.StaticEvents.ExecuteItemCollected($"No item crafted!");

            _craftingData.FailureCraftingCallback.Invoke();

            _craftingData.ResetSlots();

        }
        #endregion

    }
}
