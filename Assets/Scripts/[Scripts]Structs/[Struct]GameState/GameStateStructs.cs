using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HCC.GameState
{
    [Serializable]
    public struct InputControl
    {
        #region Fields
        [BoxGroup("Enable Input")]
        [SerializeField] private InputActionReference[] _enableReference;

        [BoxGroup("Disable Input")]
        [SerializeField] private InputActionReference[] _disableReference;
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

        public void Execute() 
        {
            Enable();
            Disable();
        }
    
        private void Enable() 
        { 
            if(_enableReference.Length <= 0) return;

            for(int i = 0; i < _enableReference.Length; i++) 
            {
                _enableReference[i].action.Enable();
            }
        }

        private void Disable() 
        { 
            if(_disableReference.Length <= 0) return;

            for(int i = 0; i < _disableReference.Length; i++) 
            {
                _disableReference[i].action.Disable();
            }

        
        }
        #endregion
    }
}
