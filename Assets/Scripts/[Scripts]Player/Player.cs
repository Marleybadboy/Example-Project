
using HCC.GameState;
using HCC.Interfaces;
using Sirenix.OdinInspector;
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

        [Inject]
        private Inventory _inventory;
        [Inject]
        private GameStateControler _gameState;

        #endregion

        #region Properties
        #endregion

        #region Functions
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

            HolderAction();
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
        #endregion
    }

   
}
