using HCC.Interfaces;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace HCC.Interactable
{
    [AddComponentMenu("HCC Components/Interactable/Interactable Object")]
    public class InteractableObject : MonoBehaviour
    {
        #region Fields

        [BoxGroup("Interaction")]
        [SerializeReference] private IInteractable _interactableType;

        [SerializeField] private SceneContext _sceneContext;

        #endregion

        #region Properties
        public IInteractable interactableType { get => _interactableType; }
        #endregion

        #region Functions
        private void Start () 
        {
            InstallInteractable();

        }
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
        private void InstallInteractable()
        {
            Type run = _interactableType.GetType();

            if(_sceneContext == null) { _sceneContext = FindObjectOfType<SceneContext>(true); }

            _sceneContext.Container.Bind(run).FromInstance(_interactableType).AsTransient().NonLazy();

            _sceneContext.Container.Inject(_interactableType);

        }

        
        #endregion
    }
}
