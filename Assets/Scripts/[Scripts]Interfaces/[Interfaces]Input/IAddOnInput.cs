
using StarterAssets;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace HCC.Interfaces
{
    public interface IAddOnInput
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Functions
        public InputAction GetPlayerInput();

        public void AssignAdditionalInput();
        public void OnInventory(InputValue action);
        public void OnPrimaryAction(InputValue action);
        public void OnEscapeAction(InputValue action);
        public void NumericAction(CallbackContext context);
        #endregion

        #region Methods
        #endregion
    }
}
