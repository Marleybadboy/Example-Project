using HCC.Interfaces;
using System;
using Zenject;

namespace HCC.Structs.Zenject
{

    [Serializable]
    public struct StandardBind : IBinder
    {
        public DiContainer Container { set => _container = value; }

        private DiContainer _container;

        public void InjectBindings<TParamInjectedType, TParamTargetType>(IBinderHolder injectedObject, IBinderHolder injectionTarget)
        {
            _container.Bind<TParamInjectedType>().FromInstance((TParamInjectedType)injectedObject.BindValue).AsSingle();
            _container.Bind<TParamTargetType>().AsCached().NonLazy();
        }
    }
}
