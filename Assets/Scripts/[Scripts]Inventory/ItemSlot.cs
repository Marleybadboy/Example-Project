using HCC.DataBase;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace HCC.GUI.InventoryUI
{
    [RequireComponent(typeof(RectTransform))]
    public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
    {

        #region Fields
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private Image _itemIcon;

        private InventoryManager _inventoryManager;
        private RectTransform _rectTransform;
        private RectTransform _rectTransformBackpack;
        private float2 _originalPos;
        private InputAction _dropAction;

        private Item _item;
        private Canvas _canvas;

        private bool _canBeDropped;
         
        #endregion

        #region Properties
        private Sprite ItemIcon { set => _itemIcon.sprite = value; }
        private bool BlockRay { set => _group.blocksRaycasts = value; }
        public Item ItemInSlot { get => _item; }
        #endregion

        #region Functions
        void Start () 
        { 
            _rectTransform = GetComponent<RectTransform>();
        }

        #endregion

        #region Methods
        public void Initialize(Item item, Canvas canvas, RectTransform backpackpos, InventoryManager manager, InputAction dropAction) 
        { 
            _item = item;
            _canvas = canvas;
            ItemIcon = item.ItemIcon;
            _rectTransformBackpack = backpackpos;
            _inventoryManager = manager;
            _dropAction = dropAction;

            AssignAction();
        }

        private void AssignAction() 
        {
            _dropAction.performed += DropAction;
        }

        private void RemoveAction() 
        {
            _dropAction.performed -= DropAction;
        }

        private void DropAction(CallbackContext context) 
        {
            if (!_canBeDropped) return;

            _inventoryManager.DropItem(_item);

             RemoveItemSlot();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPos = _rectTransform.anchoredPosition;
            BlockRay = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            bool isInParent = RectTransformUtility.RectangleContainsScreenPoint(_rectTransformBackpack, _rectTransform.position,_canvas.worldCamera);

            if (isInParent) 
            {
                _originalPos = GetNewPos(eventData);
            }

            _rectTransform.anchoredPosition = _originalPos;

            BlockRay = true;
        }

        private float2 GetNewPos(PointerEventData eventData) 
        { 
            Vector2 pos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransformBackpack, eventData.position, _canvas.worldCamera, out pos);

            Vector2 worldPosition = _rectTransformBackpack.TransformPoint(pos);
            Vector2 localPosition = _rectTransform.parent.InverseTransformPoint(worldPosition);

            return new float2(localPosition.x, localPosition.y);
        }

        public void RemoveItemSlot() 
        { 
            _inventoryManager.RemoveItemSlot(this);

            RemoveAction();

            Destroy(gameObject);
        
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _canBeDropped = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _canBeDropped = false;
        }
        #endregion
    }
}
