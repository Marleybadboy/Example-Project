using HCC.DataBase;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Fields
    [Title("Inventory",TitleAlignment = TitleAlignments.Centered, HorizontalLine = true)]
    [BoxGroup("Items")]
    [SerializeField] private List<Item> _items;
    #endregion

    #region Properties
    #endregion

    #region Functions

   // Start is called before the first frame update
    void Start()
    {
        
    }

    #endregion

    #region Methods
    #endregion 
}
