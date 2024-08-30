using HCC.DataBase;
using HCC.GUI.InventoryUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HCC.Crafting
{
/* The `CraftingSlot` class in C# represents a slot for crafting items, with functionality to assign
items, reset the slot, and handle item drops. */
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
     /// <summary>
     /// The Assign method assigns an item to a slot and displays its icon, while the ResetSlot method
     /// removes the item from the slot and hides the icon.
     /// </summary>
     /// <param name="Item">Item is a class representing an item in a game or application. It likely
     /// contains properties such as ItemIcon, which is an image or icon representing the item.</param>
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

    /// <summary>
    /// The OnDrop function in C# retrieves the item being dragged, assigns it to a new slot, and
    /// removes it from the original slot.
    /// </summary>
    /// <param name="PointerEventData">PointerEventData is a class in Unity used to pass event data to
    /// event handler functions. It contains information about the pointer event that triggered the
    /// function, such as the position of the pointer, the pointer's target object, and other relevant
    /// data. In this context, it is used to handle drag and drop</param>
    /// <returns>
    /// If the `itemSlot` is `null`, the method will return and exit early.
    /// </returns>
        public void OnDrop(PointerEventData eventData)
        {
            var itemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();

            if (itemSlot == null) return;

            Assign(itemSlot.ItemInSlot);

            itemSlot.RemoveItemSlot();
        }
        #endregion
    }
}
