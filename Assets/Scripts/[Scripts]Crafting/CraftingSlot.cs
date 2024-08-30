using HCC.DataBase;
using HCC.GUI.InventoryUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HCC.Crafting
{
    public class CraftingSlot : MonoBehaviour, IDropHandler
    {
        #region Fields
        [SerializeField] private Image _itemIconSlot;

        private Item _itemInSlot;
        #endregion

        #region Properties
        public bool IsItemInSlot { get => _itemInSlot != null; }
        private bool ShowIcon { set => _itemIconSlot.enabled = value; }
        private Sprite AssignIcon { set => _itemIconSlot.sprite = value; }
        public Item ItemInSlot { get => _itemInSlot; }
      
        #endregion

        #region Functions

        // Start is called before the first frame update
        void Start()
        {

        }

        #endregion

        #region Methods
        private void Assign(Item item) 
        { 
            _itemInSlot = item;

            AssignIcon = item.ItemIcon;

            ShowIcon = true;
        }
        public void ResetSlot() 
        { 
            _itemInSlot = null;

            ShowIcon = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var itemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();

            Debug.Log("DRopped");

            if (itemSlot == null) return;

            Assign(itemSlot.ItemInSlot);

            itemSlot.RemoveItemSlot();
        }
        #endregion
    }
}
