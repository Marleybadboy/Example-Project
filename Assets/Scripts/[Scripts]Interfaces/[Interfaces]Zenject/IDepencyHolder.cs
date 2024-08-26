

namespace HCC.Interfaces
{
    public interface IDepencyHolder<T>
    {
        #region Fields
        #endregion

        #region Properties
        public T Value { get; }
        #endregion

        #region Functions

        // Start is called before the first frame update
        void Start()
        {

        }

        #endregion

        #region Methods
        #endregion
    }

    public interface IBinderHolder
    {

        #region Fields
        public object BindValue { get; }
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        #endregion
    }
}
