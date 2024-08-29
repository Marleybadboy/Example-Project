
using HCC.GUI;
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

        public override void AssignValue(object value)
        {
        }

        public override void ResetState()
        {
        }
        #endregion
    }

    public class InventoryState : State 
    {

        #region Fields
        [Inject]
        private InventoryManager _inventoryManager;
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public override void ExecuteState()
        {
            base.ExecuteState();

            _inventoryManager.OpenUI();

            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;   

        }
        public override void BindState(DiContainer container) { }
        
        public override void AssignValue(object value)
        {
            _inventoryManager = (InventoryManager)value;
        }

        public override void ResetState()
        {
            _inventoryManager.CloseUI();

            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
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
            Debug.Log("!@##!@");
            _binderState.AssignValue(_bindValue);
        }
    }

}
