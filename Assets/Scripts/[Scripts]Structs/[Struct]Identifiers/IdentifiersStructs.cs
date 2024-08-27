using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HCC.Structs.Identifiers
{
    [Serializable]
    public struct IdentifierCreator
    {
        #region Fields
        [Space(5)]
        [BoxGroup("Add Item Identifier")]
        [SerializeField] private string _newIdentifier;

        [BoxGroup("Remove Identifier")]
        [SerializeField] private ItemIdentifier _identifier;



        private const string _folderPathToID = "Assets/Resources/Identyfication Item";
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        [BoxGroup("Add Item Identifier")]
        [Button("Add Item ID", ButtonSizes.Medium)]
        private void AddId()
        {
            ItemID idItem = Resources.Load<ItemID>("Identyfication Item/Identifiers");

            if (idItem == null) { Debug.LogError($"Can't find Identifiers in folder {_folderPathToID}"); return; }

            if (_newIdentifier == string.Empty) { Debug.LogError($"New identifier can not be empty!"); return; }

            if(IdentyfierExist(idItem)) { Debug.Log($"Identyfier Exist! Name of identyfier {_newIdentifier}");  return; }

            idItem.ItemIdentifiers.Add(_newIdentifier);

        }

        private bool IdentyfierExist(ItemID idItem) 
        {
            return idItem.ItemIdentifiers.Contains(_identifier.ItemIdentyfication);
        }

        [BoxGroup("Remove Identifier")]
        [Button("Remove Identifier", ButtonSizes.Medium)]
        private void RemoveIdentifier() 
        {
            
            ItemID idItem = Resources.Load<ItemID>("Identyfication Item/Identifiers");

            if (idItem == null) { Debug.LogError($"Can't find Identifiers in folder {_folderPathToID}"); return; }

            if (_identifier.ItemIdentyfication == string.Empty) { Debug.LogError($"Identifier is null!"); return; }

            Debug.Log("Invoke" + IdentyfierExist(idItem));


            if (IdentyfierExist(idItem)) 
            {

                

                idItem.ItemIdentifiers.Remove(_identifier.ItemIdentyfication);
            }

        }
        #endregion
    }

    [Serializable]
    public struct ItemIdentifier
    {
        #region Fields
        [ValueDropdown("GetItemsIdentifiers")]
        [SerializeField] private string _itemIdentyfication;
        #endregion

        #region Properties
        public string ItemIdentyfication { get => _itemIdentyfication; }
        #endregion

        #region Functions
        #endregion

        #region Methods
        public List<string> GetItemsIdentifiers()
        {
            ItemID idItem = Resources.Load<ItemID>("Identyfication Item/Identifiers");

            if (idItem == null) return new List<string>();

            return idItem.ItemIdentifiers;
        }
        #endregion
    }
}
