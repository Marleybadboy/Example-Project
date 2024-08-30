
using HCC.GUI;
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
            Container.Bind<NotificationManager>().AsSingle();

            Container.Bind<NotificationHolder>().FromComponentInHierarchy(true).AsCached();

            //Container.BindInstance(new InventoryManager()).NonLazy();

            Container.Bind<InventoryHolder>().FromComponentInHierarchy(true).AsCached();

            Container.Bind<IInitializable>().To<HolderNotificationInitializator<NotificationManager>>().AsSingle().NonLazy();

            Container.Bind<IInitializable>().To<HolderInventoryInitializator<InventoryManager>>().AsSingle().NonLazy();

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