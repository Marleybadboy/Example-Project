using HCC.DataBase;
using HCC.StaticEvents;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
    {
         #region Fields
          
         [Title("Inventory", TitleAlignment = TitleAlignments.Centered, HorizontalLine = true)]
         [BoxGroup("Items")]
         [SerializeField] private List<Item> _items = new();

            private const string _message = "New Item Added!";

         [Inject]
         private UsefullHolder _usefullHolder;
        #endregion

        #region Properties
        public List<Item> Items { get => _items; }
        #endregion

        #region Functions

        // Start is called before the first frame update
        void Start()
        {

        }

        #endregion

        #region Methods
        
        public void AddMulipleItem(int itemCount, Item item) 
        { 
            for(int i = 0; i < itemCount; i++) 
            {
              AddItem(item);
            }

            string massege = $"{_message} {itemCount} {item.Identyfier.ItemIdentyfication} ";

            StaticEvents.ExecuteItemCollected(massege);
        }

        public void AddMultipleItem(Item[] item) 
        { 
            for(int i = 0; i < item.Length; i++) 
            {
                AddItem(item[i]);
            }
        }
    
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }
        
        public void Drop(Item item) 
        { 
            GameObject obj = Instantiate(item.ItemPrefab);
            
            obj.transform.position = _usefullHolder.transform.position;
            
            _items.Remove(item);
        
        }

        #endregion
    }

