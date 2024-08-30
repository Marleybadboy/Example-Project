using System;
using UnityEngine;
using Zenject;

namespace HCC.GameState
{
    [Serializable]
    /* The `public abstract class State` is defining an abstract class named `State`. Abstract classes
    in C# cannot be instantiated directly and are meant to be used as base classes for other classes
    to inherit from. In this case, the `State` class serves as a base class for other specific state
    classes in a game state management system. */
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
    /// <summary>
    /// The ExecuteState function calls the Execute method of the _controlInput object.
    /// </summary>
        public virtual void ExecuteState() 
        {
            _controlInput.Execute();
        }

      /// <summary>
      /// The above code snippet defines an abstract class with methods for resetting state, binding
      /// state to a container, and assigning a value.
      /// </summary>
        public abstract void ResetState();
        public abstract void BindState(DiContainer container);
        public abstract void AssignValue(object value);
        #endregion
    }
}
