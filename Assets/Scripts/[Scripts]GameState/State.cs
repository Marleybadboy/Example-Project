using System;
using UnityEngine;
using Zenject;

namespace HCC.GameState
{
    [Serializable]
    public abstract class State
    {
        #region Fields
        [SerializeField] private InputControl _controlInput;
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public virtual void ExecuteState() 
        {
            _controlInput.Execute();
        }

        public abstract void ResetState();
        public abstract void BindState(DiContainer container);
        public abstract void AssignValue(object value);
        #endregion
    }
}
