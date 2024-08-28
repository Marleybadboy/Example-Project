using HCC.Structs.Identifiers;
using Sirenix.OdinInspector;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace HCC.DataBase
{
    public abstract class Item : ScriptableObject
    {
        #region Fields

        [BoxGroup("Item Data")]
        [SerializeField] private ItemIdentifier _identifier;

        [BoxGroup("Item Data")]
        [PreviewField]
        [SerializeField] private Sprite _itemIcon;

        [BoxGroup("Item Data")]
        [SerializeField] private string _itemPrefabAddress;

        [Space(5)]
        [BoxGroup("Item ID Creator")]
        [SerializeField] private IdentifierCreator _identyfierCreator;

        private const string _folderPathToID = "Assets/Resources/Identyfication Item";
        #endregion

        #region Properties
        public ItemIdentifier Identyfier { get => _identifier; set => _identifier = value; }
        #endregion

        #region Functions
        protected virtual void OnEnable() 
        {
            #if UNITY_EDITOR

            CheckItemId();

            #endif

        }
        #endregion

        #region Methods

        private void CheckItemId() 
        {
            bool created = ItemIdDataCreated();

            if (created) return; 
            
            AssetDatabase.CreateAsset(new ItemID(), $"{_folderPathToID}/Identifiers.asset");
            AssetDatabase.SaveAssets();
        
        }

        private bool ItemIdDataCreated()
        {
            if (!Directory.Exists(_folderPathToID))
            {
                Directory.CreateDirectory(_folderPathToID);

                Debug.Log("Created directory: " + _folderPathToID);

                return false;
            }

            ItemID idItem = Resources.Load<ItemID>("Identyfication Item/Identifiers");

            if (idItem == null) return false;


            return true;
        }
        #endregion
    }

}
