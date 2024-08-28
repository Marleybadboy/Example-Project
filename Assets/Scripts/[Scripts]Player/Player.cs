
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

        // Start is called before the first frame update
        void Start()
        {

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
            throw new System.NotImplementedException();
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
        #endregion
    }

   
}
