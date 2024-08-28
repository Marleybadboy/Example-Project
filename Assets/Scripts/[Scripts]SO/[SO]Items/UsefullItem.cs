


using HCC.Structs.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HCC.DataBase
{
    [CreateAssetMenu(fileName = "UsefullItem SO", menuName = "Item/UsefullItem SO")]
    public class UsefullItem : Item
    {
        #region Fields

        [BoxGroup("Item Data")]
        [SerializeField] private ItemIdentifier _notIteractableIdentifier;

        #endregion

        #region Properties
        public ItemIdentifier NotIteractableIdentifier { get => _notIteractableIdentifier; }
        #endregion

        #region Functions
        #endregion

        #region Methods
        #endregion
    }
}
