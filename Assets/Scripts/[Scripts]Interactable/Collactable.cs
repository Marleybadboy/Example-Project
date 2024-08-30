using HCC.DataBase;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

public class Collactable : IInteractable
{

    #region Fields
    [BoxGroup("Item")]
    [SerializeField] private Item _itemToAdd;
    #endregion

    #region Properties
    public Item ItemToAdd { get => _itemToAdd; }
    #endregion

    #region Functions
    #endregion

    #region Methods
    public void OnCollisionEnter(Collision collisionInfo)
    {
       
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
       
    }

    public void OnTriggerExit(Collider other)
    {
        
    }
    #endregion 
}
