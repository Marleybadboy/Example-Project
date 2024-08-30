using HCC.DataBase;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "Mining Data", menuName = "Interactable SO/MiningData")]
public class MiningData : InteractableData
{
    [BoxGroup("Data")]
    [SerializeField, Range(0f, 5f)] private float _hitDelay;

    public float HitDelay { get => _hitDelay; }

}
