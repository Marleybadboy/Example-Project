using HCC.DataBase;
using HCC.GUI.InventoryUI;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace HCC.Structs.GUI
{
    #region Notyfication

    [Serializable]
    public struct Notification : IUIDataHolder
    {
        #region Fields
        [BoxGroup("Elements")]
        [SerializeField] private RectTransform _notificationObj;
        [BoxGroup("Elements")]
        [SerializeField] private TextMeshProUGUI _notificationText;
        [BoxGroup("Elements")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Space(10)]
        [BoxGroup("Animation")]
        [SerializeField] private float _duration;
        [BoxGroup("Animation")]
        [SerializeField] private float3 _firstStep;
        [BoxGroup("Animation")]
        [SerializeField] private float3 _secondStep;



        #endregion

        #region Properties
        public float CanvasAlpha { set => _canvasGroup.alpha = value; }
        public float Duration { get => _duration; }
        public string NotificationText { set => _notificationText.text = value; }
        public float2 NotificationObjPos { set => _notificationObj.anchoredPosition = value; }
        public RectTransform NotificationObj { get => _notificationObj; set => _notificationObj = value; }
        public CanvasGroup CanvasGroup { get => _canvasGroup; }
        #endregion

        #region Functions
        #endregion

        #region Methods
        public Vector3[] CreateWaypoints()
        {
            return new Vector3[] { _firstStep, _secondStep };
        }
        #endregion
    }

    #endregion

    #region GUIInventoryData
    [Serializable]
    public struct GUIInventoryData : IUIDataHolder
    {
        [BoxGroup("Elements")]
        [SerializeField] private GameObject _slotPrefab;
        [BoxGroup("Elements")]
        [SerializeField] private CanvasGroup _inventoryGroup;
        [BoxGroup("Elements")]
        [SerializeField] private RectTransform _backpackInventory;
        [BoxGroup("Elements")]
        [SerializeField] private RectTransform _parentInventory;
        [BoxGroup("Elements")]
        [SerializeField] private Canvas _mainCanvas;

        public bool ActiveInventory 
        { set 
            {
                if (value) 
                { 
                    _inventoryGroup.alpha = 1.0f;
                    return;
                }

                _inventoryGroup.alpha = 0f;
            }
        }

        private float2 GetRandomPos() 
        {    
            float2 pos = new float2
            {
                x = UnityEngine.Random.Range(_backpackInventory.rect.xMin, _backpackInventory.rect.xMax),
                y = UnityEngine.Random.Range(_backpackInventory.rect.yMin, _backpackInventory.rect.yMax)
            };

            pos.x = Mathf.Clamp(pos.x, _backpackInventory.rect.xMin, _backpackInventory.rect.xMax);
            pos.y = Mathf.Clamp(pos.y, _backpackInventory.rect.yMin, _backpackInventory.rect.yMax);

            return pos;
        }

        public ItemSlot[] CreateSlots(List<Item> items) 
        { 
            List<ItemSlot> slots = new List<ItemSlot>();

            for(int i = 0; i < items.Count; i++) 
            { 
                var slot = CreateSlot(items[i]);
                
                slots.Add(slot);
            }
            
            return slots.ToArray();
        }
        
        private ItemSlot CreateSlot(Item item) 
        {
            GameObject obj = UnityEngine.Object.Instantiate(_slotPrefab, _parentInventory);

            var transform = obj.GetComponent<RectTransform>();
            var slot = obj.GetComponent<ItemSlot>();

            transform.anchoredPosition = GetRandomPos();
            
            slot.Initialize(item, _mainCanvas);

            return slot;
        } 
    
    }

    #endregion
}
