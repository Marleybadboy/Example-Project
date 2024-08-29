using HCC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HCC.GUI
{
    public abstract class GUIHolder<TManagerParam,TDataParam> : MonoBehaviour where TManagerParam : GUIManager where TDataParam : IUIDataHolder
    {
        #region Fields

        [BoxGroup("UI")]
        [SerializeField] TDataParam _dataParam;

        [Inject]
        private TManagerParam _managerParam;

        #endregion

        #region Properties
        public TManagerParam Manager { get => _managerParam; }
        public TDataParam DataParam { get => _dataParam; }
        #endregion

        #region Functions

        // Start is called before the first frame update
        public virtual void Start()
        {
            _managerParam.SetData(_dataParam);
        }

        
        public virtual void AssignManager(GUIManager manager) 
        {
            _managerParam = (TManagerParam)manager;
        }

        #endregion

        #region Methods
        #endregion
    }

    public class HolderNotificationInitializator<TManagerParam> : IInitializable where TManagerParam : GUIManager
    {

        private readonly TManagerParam _managerParam;
        private readonly NotificationHolder _dataHolderParam;

        public HolderNotificationInitializator(TManagerParam managerParam, NotificationHolder dataParam)
        {
            _managerParam = managerParam;
            _dataHolderParam = dataParam;
        }

        public void Initialize()
        {
            _dataHolderParam.AssignManager(_managerParam);
        }


    }

    public class HolderInventoryInitializator<TManagerParam> : IInitializable where TManagerParam : GUIManager
    {
        private readonly TManagerParam _managerParam;
        private readonly InventoryHolder _dataHolderParam;

        public HolderInventoryInitializator(TManagerParam managerParam, InventoryHolder dataParam)
        {
            _managerParam = managerParam;
            _dataHolderParam = dataParam;
        }

        public void Initialize()
        {
            _dataHolderParam.AssignManager(_managerParam);
        }
    }
}
