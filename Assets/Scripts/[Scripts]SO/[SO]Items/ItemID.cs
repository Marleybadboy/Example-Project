using System.Collections.Generic;
using UnityEngine;

public class ItemID : ScriptableObject
{
    #region Fields
    [SerializeField] private List<string> _itemsIdentifiers = new List<string>();
    #endregion

    #region Properties
    [SerializeField] public List<string> ItemIdentifiers { get => _itemsIdentifiers; set => _itemsIdentifiers = value; }
    #endregion

    #region Functions
    #endregion

    #region Methods
    #endregion
}
