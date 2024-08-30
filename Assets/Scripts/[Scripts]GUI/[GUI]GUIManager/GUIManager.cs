using HCC.DataBase;
using HCC.Interfaces;
using UnityEngine;

namespace HCC.GUI
{
  /* The `public abstract class GUIManager` in the provided C# code snippet is defining an abstract
  class named `GUIManager`. Here's what it signifies: */
    public abstract class GUIManager
    {
        #region Fields
        private IUIDataHolder _uiData;
        #endregion

        #region Properties
        public IUIDataHolder UIData { get => _uiData; set => _uiData = value; }
        #endregion

        #region Functions
        #endregion

        #region Methods
    /// <summary>
    /// The code defines a C# class with a method to set data and abstract methods to open and close a
    /// UI.
    /// </summary>
    /// <param name="IUIDataHolder">IUIDataHolder is likely an interface or a base class that defines
    /// the structure for holding UI data. It could contain properties or methods related to managing
    /// and accessing data used by the UI components. In the provided code snippet, the SetData method
    /// takes an object that implements the IUIDataHolder interface</param>
        public virtual void SetData(IUIDataHolder holder)
        {
            _uiData = holder;
        }

        public abstract void OpenUI();
        public abstract void CloseUI();
        #endregion
    }
}
