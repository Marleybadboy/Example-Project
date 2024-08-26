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
        [SerializeField] private string _interactObjectTag;

        #endregion

        #region Properties
        public int MaxHits { get => _maxHits; }
        public string InteractTag { get => _interactObjectTag; }
        #endregion

    }
}
