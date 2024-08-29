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

    [Serializable]
    public struct MonoFromComponentInHierarchyBind : IBinder 
    {

        public DiContainer Container { set => _container = value; }

        private DiContainer _container;

        public void InjectBindings<TParamInjectedType, TParamTargetType>(IBinderHolder injectedObject, IBinderHolder injectionTarget)
        {
            _container.Bind<TParamInjectedType>().FromComponentInHierarchy(true).AsSingle();

            _container.Bind<TParamTargetType>().FromComponentInHierarchy(true).AsCached();
        }
    }

    [Serializable]
    public struct BindToAllMonoTargets : IBinder
    {
        public DiContainer Container { set => _container = value; }

        private DiContainer _container;

        public void InjectBindings<TParamInjectedType, TParamTargetType>(IBinderHolder injectedObject, IBinderHolder injectionTarget)
        {
            _container.Bind<TParamInjectedType>().FromComponentInHierarchy(true).AsCached();

            _container.Bind<TParamTargetType>().FromComponentsInHierarchy().AsTransient();
        }
    }

    [Serializable]
    public struct SingleAbstractToMonoBind : IBinder
    {
        public DiContainer Container { set => _container = value; }

        private DiContainer _container;

        public void InjectBindings<TParamInjectedType, TParamTargetType>(IBinderHolder injectedObject, IBinderHolder injectionTarget)
        {

            

            _container.Bind<TParamInjectedType>().To(injectedObject.BindValue.GetType()).AsSingle().NonLazy();

            _container.Bind<TParamTargetType>().FromComponentInHierarchy(true).AsSingle();

        }
    }
}
