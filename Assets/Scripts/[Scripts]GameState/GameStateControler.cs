using System;
using UnityEngine;
using Zenject;

namespace HCC.GameState
{
    public class GameStateControler
    {
        #region Fields
        [SerializeReference] private State[] _statesSettings;


        private State _currentState;
        private State _previousState;

       
        private Inventory _inventory;


        #endregion

        #region Properties
        public State[] States => _statesSettings;
        #endregion

        #region Functions

        #endregion

        #region Methods
        public void SetInventory(Inventory inventory) 
        { 
            _inventory = inventory;
        }

        public void ChangeState(State state) 
        {

            if (_statesSettings.Length <= 0) return;

            for (int i = 0;  i < _statesSettings.Length; i++) 
            { 
                if(_statesSettings[i].GetType() == state.GetType()) 
                {
                     CheckState(_statesSettings[i]);

                    _statesSettings[i].ExecuteState();
                    
                    return;
                }
            
            }
        
        }

        public void BackToPrevious() 
        {
            Debug.Log(_previousState);

            if (_currentState is DefaultState || _previousState == null) return;

            _currentState.ResetState();

            ChangeState(_previousState);
        }

        private void CheckState(State state) 
        {

            if (_previousState == null) 
            { 
                _previousState = GetState(new DefaultState()); 
                _currentState = state;
                return; 
            }

            _previousState = _currentState;

            _currentState = state;
        }

        public State GetState(State state) 
        { 
            return Array.Find(_statesSettings, s => s.GetType() == state.GetType());
        }

        public bool InvetoryExist() 
        { 
            return _inventory != null;
        }
        #endregion
    }

    public class ControleInjection : IInitializable
    {
        private readonly GameStateControler _gameState;
        private readonly Inventory _inventory;


        public ControleInjection(GameStateControler controler, Inventory inventory)
        {
            _gameState = controler;
            _inventory = inventory;
        }

        public void Initialize()
        {
            _gameState.SetInventory(_inventory);
        }
    }
}