
using Zenject;

namespace HCC.Interfaces
{
    public interface IBinder
    {
        #region Fields
        public DiContainer Container { set; }
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public void InjectBindings<TParamInjectedType, TParamTargetType>(IBinderHolder injectedObject, IBinderHolder injectionTarget); 
        #endregion
    }
}
