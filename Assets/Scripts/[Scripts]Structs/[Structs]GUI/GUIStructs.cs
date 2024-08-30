using DG.Tweening;
using HCC.Crafting;
using HCC.DataBase;
using HCC.GUI;
using HCC.GUI.InventoryUI;
using HCC.Interfaces;
using HCC.Structs.Identifiers;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        [BoxGroup("Elements")]
        [SerializeField] private InputActionReference _dropReference;

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
            Vector2 pos = new float2
            {
                x = UnityEngine.Random.Range(_backpackInventory.rect.xMin, _backpackInventory.rect.xMax),
                y = UnityEngine.Random.Range(_backpackInventory.rect.yMin, _backpackInventory.rect.yMax)
            };

            pos.x = Mathf.Clamp(pos.x, _backpackInventory.rect.xMin, _backpackInventory.rect.xMax);
            pos.y = Mathf.Clamp(pos.y, _backpackInventory.rect.yMin, _backpackInventory.rect.yMax);

            Vector2 worldPosition = _backpackInventory.TransformPoint(pos);
            Vector2 localPosition = _parentInventory.InverseTransformPoint(worldPosition);

            return new float2(localPosition.x,localPosition.y);
        }

        public ItemSlot[] CreateSlots(List<Item> items, InventoryManager manager) 
        { 
            List<ItemSlot> slots = new List<ItemSlot>();

            for(int i = 0; i < items.Count; i++) 
            { 
                var slot = CreateSlot(items[i], manager);
                
                slots.Add(slot);
            }
            
            return slots.ToArray();
        }
        
        private ItemSlot CreateSlot(Item item, InventoryManager manager) 
        {
            GameObject obj = UnityEngine.Object.Instantiate(_slotPrefab, _parentInventory);

            var transform = obj.GetComponent<RectTransform>();
            var slot = obj.GetComponent<ItemSlot>();

            transform.anchoredPosition = GetRandomPos();
            
            slot.Initialize(item, _mainCanvas,_backpackInventory, manager, _dropReference);

            return slot;
        } 
    
    }

    #endregion

    #region CraftingData

    [Serializable]
    public struct CraftingData : IUIDataHolder
    {
        [BoxGroup("Elements")]
        [SerializeField] private Button _craftingButton;

        [BoxGroup("Elements")]
        [SerializeField] private Slider _craftingSlider;

        [BoxGroup("Elements"), Range(0f, 10f)]
        [SerializeField] private float _craftingDuration;

        [BoxGroup("Elements")]
        [SerializeField] private CanvasGroup _slotGroup;

        [BoxGroup("Elements/First Slots")]
        [SerializeField] private CraftingSlot[] _craftingSlots;

        [BoxGroup("Elements/Recipes")]
        [SerializeField] private Recipe[] _craftingRecipes;

        [BoxGroup("Elements/Failure")]
        [SerializeField] private UnityEvent _failureCraftingCallback;

        public delegate void OnCraftingDelegete();



        public UnityEvent FailureCraftingCallback { get => _failureCraftingCallback; }
        public float ActiveGroup { set => _slotGroup.alpha = value; }
        
        public void AssignCraftingButton(OnCraftingDelegete craftingDelegete) 
        {
            _craftingButton.onClick.AddListener(() => craftingDelegete());
        }

        public void ResetCallback() 
        {
            _craftingButton.onClick.RemoveAllListeners();
        }

        public Tween ExecuteCraftingTween() 
        {
            _craftingButton.interactable = false;

            return _craftingSlider.DOValue(1, _craftingDuration);
        }

        public void ResetSlider() 
        {
            _craftingSlider.value = 0;

            _craftingButton.interactable = true;
        }


        public void ResetSlots()
        {
            if (_craftingSlots.Length <= 0) return;

            for(int i = 0;  i < _craftingSlots.Length; i++) 
            {
                _craftingSlots[i].ResetSlot();
            }
        }

        private CraftingCheckData CreateData() 
        { 
            List<Item> itemList = new List<Item>();

            for(int i = 0; i < _craftingSlots.Length; i++) 
            {
                if (_craftingSlots[i].ItemInSlot == null) continue;

                itemList.Add(_craftingSlots[i].ItemInSlot);       
            }

            CraftingCheckData data = new CraftingCheckData(itemList.ToArray());

            data.ExecuteCreate();

            return data;   
        }

        public bool FindRecipe(out Recipe recipe) 
        {
            CraftingCheckData data = CreateData();

            for (int i = 0; i < _craftingRecipes.Length; i++) 
            {
                bool match = _craftingRecipes[i].CraftDataMatch(data);

                if (!match) 
                {
                    continue;
                }

                recipe = _craftingRecipes[i];
                return true;
            
            }
            recipe = null;
            return false;
        }

        public void CreateDataForRecipes() 
        {
            for (int i = 0; i < _craftingRecipes.Length; i++) 
            {
                _craftingRecipes[i].EstablishData();
            }
        
        }
    }

    public struct CraftingCheckData 
    {
        private readonly Item[] _itemsData;

        public CraftingCheck[] _checkData;

        public CraftingCheckData(Item[] itemsData) 
        { 
            _itemsData = itemsData;
            
            _checkData = null;
        }


        public void ExecuteCreate() 
        {
            _checkData = CreateData(_itemsData);
        }

        private CraftingCheck[] CreateData(Item[] items) 
        {
            List<CraftingCheck> itemListSlot = new List<CraftingCheck>();

            if (items.Length <= 0) return itemListSlot.ToArray();

            for (int i = 0; i < items.Length; ++i)
            {
                ItemIdentifier id = items[i].Identyfier;

                if (itemListSlot.Exists(item => id.ItemIdentyfication == item.ID))
                {
                    continue;
                }

                CraftingCheck check = new CraftingCheck
                {
                    Item = items[i],
                    ItemCount = Array.FindAll(items, slot => slot.Identyfier.ItemIdentyfication == id.ItemIdentyfication).Length

                };

                itemListSlot.Add(check);
            }

            return itemListSlot.ToArray();

        }

    }

    public struct CraftingCheck 
    {
        public Item Item;
        public int ItemCount;

        public string ID { get => Item.Identyfier.ItemIdentyfication; }
    
    }

    #endregion
}
