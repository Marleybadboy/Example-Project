
using HCC.GameState;
using HCC.Interactable;
using HCC.Interfaces;
using HCC.Structs.Jobs;
using Sirenix.OdinInspector;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HCC.Player
{
    public class Player : MonoBehaviour, IAddOnInput
    {
        #region Fields

        [BoxGroup("Interaction")]
        [SerializeField] private Transform _holdPoint;
        [BoxGroup("Interaction")]
        [SerializeField] private InputActionReference _referenceNumeric;
        [BoxGroup("Interaction/Collactable Find"), Range(1f, 10000f)]
        [SerializeField] private float _distanceFinder;

        [Inject]
        private Inventory _inventory;
        [Inject]
        private GameStateControler _gameState;
        [Inject]
        private UsefullHolder _usefullHolder;

        private InputAction _numericAction;
        private float3 _origin = new float3(0.5f, 0.5f, 0f);
        private InteractableObject _interactable;

        private Action _primaryAction;
        

        #endregion

        #region Properties
        #endregion

        #region Functions
        private void Start()
        {
            AssignAdditionalInput();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() 
        {
            FindCollactable();
        }


        private void OnDestroy()
        {
            DisableActions();
        }

        #endregion

        #region Methods
        [Button]
        private void CheckInjection() 
        { 
            if(_inventory != null) { Debug.Log("Jest inventory"); }

            if(_gameState != null) { Debug.Log(_gameState.States.Length + " " + _gameState.InvetoryExist()); }
        }

        public void OnInventory(InputValue action)
        {
            if (!action.isPressed) return;

            _gameState.ChangeState(new InventoryState());
        }

        public void OnPrimaryAction(InputValue action)
        {
            if (!action.isPressed) return;

            switch (_usefullHolder.State) 
            {
                case UsefullHolder.UsefullState.Item:
                HolderAction();
                break;

                case UsefullHolder.UsefullState.Empty:
                CollectObject();
                break;

            }
        }

        private void HolderAction() 
        { 
            Animation anim = _holdPoint.GetComponentInChildren<Animation>();

            if (anim == null) return;

            anim.Play();
        
        }

        public void OnEscapeAction(InputValue action)
        {
            if (!action.isPressed) return;

            _gameState.BackToPrevious();
        }


        public InputAction GetPlayerInput()
        {
            return _referenceNumeric.action;
        }

        public void AssignAdditionalInput()
        {
            _numericAction = GetPlayerInput();

            _numericAction.performed += NumericAction;

            _numericAction.Enable();
        }

        private void DisableActions() 
        { 
            _numericAction?.Disable();

            _numericAction.performed -= NumericAction;
        }

        public void NumericAction(InputAction.CallbackContext context)
        {
            int value;
            
            int.TryParse(context.control.name, out value);

            if(value == 0) return;  

            _usefullHolder.AssignByNumericKey(value);
        }

        private void CollectObject() 
        {
            if (_interactable == null) return;

            if(_interactable.interactableType is Collactable collactable) 
            {
                _inventory.AddItem(collactable.ItemToAdd);

                Destroy(_interactable.gameObject);
            }


        }

        private void FindCollactable() 
        {

            if (_usefullHolder.State == UsefullHolder.UsefullState.Item) return;

            RaycastFinder finder = new RaycastFinder(_origin, _distanceFinder);

            if (finder.Hit.collider == null) return;

            if (finder.Hit.collider.CompareTag("Collactable")) 
            {
                _interactable = finder.Hit.collider.GetComponent<InteractableObject>();

                if (_interactable == null) return;

            }


        }
        #endregion
    }

   
}
