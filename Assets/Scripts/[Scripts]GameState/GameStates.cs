
using HCC.GUI;
using UnityEngine;
using Zenject;

namespace HCC.GameState
{
   /* The DefaultState class in C# inherits from State and contains methods for binding state,
   assigning values, and resetting the state. */
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

 /* The `InventoryState` class in the provided C# code is a class that inherits from the `State` class.
 It contains specific functionality related to managing the inventory state within a game. Here is a
 breakdown of what the `InventoryState` class is doing: */
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
/// <summary>
/// The ExecuteState function opens the inventory UI and sets the cursor visibility and lock state in a
/// C# script.
/// </summary>
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
/// <summary>
/// The ResetState function closes the inventory UI, hides the cursor, and locks the cursor in place.
/// </summary>

        public override void ResetState()
        {
            _inventoryManager.CloseUI();

            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion

    }

  /* The InitializeStateBinder class initializes a State object with a specified value. */
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
