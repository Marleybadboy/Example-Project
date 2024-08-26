using HCC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HCC.Interactable
{
    [AddComponentMenu("HCC Components/Interactable/Interactable Object")]
    public class InteractableObject : MonoBehaviour
    {
        #region Fields

        [BoxGroup("Interaction")]
        [SerializeReference] private IInteractable _interactableType;

        #endregion

        #region Properties
        #endregion

        #region Functions
        private void OnTriggerEnter(Collider other)
        {
            _interactableType.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _interactableType.OnTriggerExit(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _interactableType.OnCollisionEnter(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            _interactableType.OnCollisionExit(collision);
        }

        #endregion

        #region Methods
        #endregion
    }
}
