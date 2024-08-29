using HCC.GUI;
using HCC.Interactable;
using HCC.Interfaces;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace HCC.Structs.Zenject
{

    [Serializable]
    public struct BindHolder
    {
        [BoxGroup("Injection")]
        [Tooltip("Choose data to inject")]
        [SerializeReference] private IBinderHolder _depencyToInject;

        [BoxGroup("Injection")]
        [Tooltip("Choose data where object is injected")]
        [SerializeReference] private IBinderHolder _depencyObjectInjection;

        [BoxGroup("Bind Type")]
        [Tooltip("Choose how to bind data")]
        [SerializeReference] private IBinder _binder;

        public void StartBind(DiContainer container) 
        { 
            _binder.Container = container;

            var injectionType = _depencyToInject.BindValue.GetType();
            var targetType = _depencyObjectInjection.BindValue.GetType();

            var method = typeof(IBinder).GetMethod(nameof(_binder.InjectBindings)).MakeGenericMethod(injectionType,targetType);

            method.Invoke(_binder, new object[] {_depencyToInject,_depencyObjectInjection});
        }
    }

    [Serializable]
    public struct StringHolder : IDepencyHolder<string>, IBinderHolder
    {
        [SerializeField] private string _value;
        public string Value => _value;

        public object BindValue => _value;
    }

    [Serializable]
    public struct IntHolder : IDepencyHolder<int>, IBinderHolder
    {
        [SerializeField] private int _value;
        public int Value => _value;

        public object BindValue => _value;
    }

    [Serializable]
    public struct InteractableHolder : IDepencyHolder<InteractableObject>, IBinderHolder
    {
        [SerializeField] private InteractableObject _value;
        public InteractableObject Value => _value;

        public object BindValue => _value;
    }

    [Serializable]
    public struct MonoHolder : IDepencyHolder<MonoBehaviour>, IBinderHolder
    {
        [SerializeField] private MonoBehaviour _value;

        public MonoBehaviour Value => _value;

        public object BindValue => _value;
    }

    [Serializable]
    public struct GUIManagerHolder : IDepencyHolder<GUIManager>, IBinderHolder
    {
        [SerializeReference] private GUIManager _value;
        
        public object BindValue => _value;

        GUIManager IDepencyHolder<GUIManager>.Value => _value;
    }

    [Serializable]
    public struct GUIHolderBinder : IDepencyHolder<NotificationHolder>, IBinderHolder
    {
        [SerializeField] private NotificationHolder _value;

        public object BindValue => _value;

        public NotificationHolder Value => _value;

    }
}
