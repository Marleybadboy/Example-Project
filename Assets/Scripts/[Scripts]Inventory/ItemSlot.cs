using HCC.DataBase;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HCC.GUI.InventoryUI
{
    [RequireComponent(typeof(RectTransform))]
    public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
    {

        #region Fields
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private Image _itemIcon;


        private RectTransform _rectTransform;
        private RectTransform _rectTransformParent;
        private float2 _originalPos;

        private Item _item;
        private Canvas _canvas;
         
        #endregion

        #region Properties
        private Sprite ItemIcon { set => _itemIcon.sprite = value; }
        private bool BlockRay { set => _group.blocksRaycasts = value; }
        #endregion

        #region Functions
        void Start () 
        { 
            _rectTransform = GetComponent<RectTransform>();

            _rectTransformParent = GetComponentInParent<RectTransform>();
        }

     
        #endregion

        #region Methods
        public void Initialize(Item item, Canvas canvas) 
        { 
            _item = item;
            _canvas = canvas;
            ItemIcon = item.ItemIcon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPos = _rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
             BlockRay = false;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            bool isInParent = RectTransformUtility.RectangleContainsScreenPoint(_rectTransformParent, _rectTransform.position);

            if (isInParent) 
            { 
                _rectTransform.anchoredPosition = _originalPos;
            }

            BlockRay = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           
        }
        #endregion
    }
}
