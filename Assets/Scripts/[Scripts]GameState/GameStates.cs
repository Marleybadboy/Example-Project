using HCC.GameState;
using UnityEngine;
using Zenject;

namespace HCC.GameState
{
    public class DefaultState : State
    {
        
        #region Fields
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
        public override void BindState(DiContainer container) { }

        public override void AssignValue<T>(T value)
        {
        }
        #endregion
    }

    public class InventoryState: State 
    {
      
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public override void BindState(DiContainer container) { }
        public override void AssignValue<T>(T value)
        {
            
        }
        #endregion

    }

    public class InitializeStateBinder<TBinderState, TBindValue> : IInitializable where TBinderState : State
    {
        private readonly TBinderState _binderState;
        private readonly TBindValue _bindValue;

        public InitializeStateBinder(TBinderState binderState, TBindValue bindValue)
        {
            _binderState = binderState;
            _bindValue = bindValue;
        }

        public void Initialize()
        {
            _binderState.AssignValue(_bindValue);
        }
    }

}
