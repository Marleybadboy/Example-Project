using HCC.Structs.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HCC.DataBase
{
    public abstract class InteractableData : ScriptableObject
    {
        #region Fields

        [Title("Interactable Global Data", TitleAlignment = TitleAlignments.Centered, HorizontalLine = true)]
        [BoxGroup("Data")]
        [SerializeField, Range(1, 50)] private int _maxHits = 1;

        [BoxGroup("Data")]
        [SerializeField] private ItemIdentifier _interactObjectIdentifier;

        [BoxGroup("Data")]
        [SerializeField] private Item _itemToAdd;

        [BoxGroup("Data")]
        [MinMaxSlider(1,10,true)]
        [SerializeField] private Vector2 _minMaxValueToAdd;



        #endregion

        #region Properties
        public int MaxHits { get => _maxHits; }
        public string InteractTag { get => _interactObjectIdentifier.ItemIdentyfication; }
        public int ItemValue { get => RandomValueItem();}
        public Item ItemToAdd { get =>  _itemToAdd; }
        #endregion

        #region Methods
        private int RandomValueItem() 
        { 
            return Random.Range(Mathf.FloorToInt(_minMaxValueToAdd.x), Mathf.FloorToInt(_minMaxValueToAdd.y));
        }
        #endregion

    }
}
