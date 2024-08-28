using HCC.Structs.Identifiers;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;


namespace HCC.DataBase
{
    [CreateAssetMenu(fileName = "Cut Data", menuName = "Interactable SO/CutData")]
    public class CutData : InteractableData
    {
        #region Fields

        [BoxGroup("Data")]
        [SerializeField, Range(0f, 5f)] private float _hitDelay;

        [BoxGroup("Data")]
        [SerializeField] private float3 _forcePush;

        [BoxGroup("Data")]
        [SerializeField] private Material _afterCutMaterial;

        #endregion

        #region Properties
        public float HitDelay { get => _hitDelay; }
        public float3 ForcePush { get => _forcePush; }
        public Material AfterCutMaterial { get => _afterCutMaterial; }
        #endregion


    }
}
