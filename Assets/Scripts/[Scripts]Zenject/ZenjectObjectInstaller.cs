using HCC.Structs.Zenject;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HCC.Zenject.Installers
{
    public abstract class ZenjectObjectInstaller : MonoInstaller
    {
        [Title("Injection Installer", TitleAlignment = TitleAlignments.Centered, HorizontalLine = true)]
        [SerializeField] private BindHolder[] _bindData;

        #region Methods
        public override void InstallBindings()
        {
            if (_bindData.Length >= 0) return;

            for(int i =0;  i < _bindData.Length; i++) 
            {
                _bindData[i].StartBind(Container);
            }
        }

        public virtual void BindType(DiContainer container)
        {

        }
        #endregion
    }
}