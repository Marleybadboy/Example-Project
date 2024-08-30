using System;
using UnityEngine;
using Zenject;

namespace HCC.GameState
{/* The class `GameStateControler` manages game states and inventory in a C# program. */

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

     /// <summary>
     /// The SetInventory method sets the inventory, while the ChangeState method changes the state
     /// based on the provided state parameter.
     /// </summary>
     /// <param name="Inventory">An inventory object that likely contains information about items,
     /// quantities, and possibly other related data.</param>
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
/// <summary>
/// The code snippet contains methods for managing states and checking inventory existence in a C#
/// program.
/// </summary>
/// <returns>
/// The `InvetoryExist` method is returning a boolean value, indicating whether the `_inventory`
/// variable is not null.
/// </returns>

        public void BackToPrevious() 
        {
           

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