using HCC.DataBase;
using HCC.Structs.Identifiers;
using UnityEngine;


[AddComponentMenu("HCC Components/Interactable/Item Object")]
public class ItemMonoComponent : MonoBehaviour
{
    #region Fields
    [SerializeField] private Item _itemType;

    private ItemIdentifier _previousItemIdentyfier;
    #endregion

    #region Properties
    public Item ItemType { get => _itemType; set => _itemType = value; }
    #endregion

    #region Functions

   // Start is called before the first frame update
    void Start()
    {
        ChangeObjectIdentifier();
    }

    private void OnDestroy()
    {
        RestoreIdentyfier();
    }
    #endregion

    #region Methods
    public void ChangeObjectIdentifier() 
    { 
        if(_itemType is UsefullItem usefull) 
        {
            _previousItemIdentyfier = _itemType.Identyfier;
            
            _itemType.Identyfier = usefull.NotIteractableIdentifier;
        }
    }

    public void RestoreIdentyfier() 
    {
        _itemType.Identyfier = _previousItemIdentyfier;
    }
    #endregion 
}
