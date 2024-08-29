using HCC.DataBase;
using HCC.Interfaces;
using UnityEngine;

namespace HCC.GUI
{
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
        public virtual void SetData(IUIDataHolder holder)
        {
            _uiData = holder;
        }

        public abstract void OpenUI();
        public abstract void CloseUI();
        #endregion
    }
}
