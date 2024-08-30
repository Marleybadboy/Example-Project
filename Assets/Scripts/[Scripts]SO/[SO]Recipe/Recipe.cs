using HCC.Structs.GUI;
using HCC.Structs.Identifiers;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HCC.DataBase
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Recipe SO/Standard Recipe")]
    public class Recipe : ScriptableObject
    {
        #region Fields

        [BoxGroup("Items")]
        [SerializeField] private Item[] _itemsToCraft;
        [BoxGroup("Items")]
        [SerializeField] private Item _itemCrafted;

        [BoxGroup("Items/Probability"), Range(1, 100)]
        [Tooltip("Probability of creating item in %")]
        [SerializeField] private int _probability;

        private CraftingCheckData _checkDataRecipe;
        #endregion

        #region Properties
        public float Probality { get => _probability / 100; }
        public Item CreatedItem { get => _itemCrafted; }
        public Item[] ItemsToCraft { get => _itemsToCraft; }
        #endregion

        #region Functions
        #endregion

        #region Methods
        public void EstablishData() 
        { 
            CraftingCheckData checkData = new CraftingCheckData(_itemsToCraft);

            checkData.ExecuteCreate();

            _checkDataRecipe = checkData;
        
        }

        public bool CraftDataMatch(CraftingCheckData match) 
        { 
            if(_checkDataRecipe._checkData.Length != match._checkData.Length) return false;

            List<CraftingCheck> checkList = match._checkData.ToList();

            for (int i = 0;  i < _checkDataRecipe._checkData.Length; i++) 
            {
                string id = _checkDataRecipe._checkData[i].ID;

                bool exist = checkList.Exists(matches => matches.ID == id);

                if(!exist) 
                { 
                    return false;
                }

                CraftingCheck craftingCheck = checkList.Find(matches => matches.ID == id);

                if(craftingCheck.ItemCount != _checkDataRecipe._checkData[i].ItemCount) 
                { 
                    return false;
                }


            }
            return true;
        }
        #endregion
    }
}
