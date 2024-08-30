using HCC.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HCC.GUI
{
    /* The `public abstract class GUIHolder<TManagerParam,TDataParam> : MonoBehaviour where
    TManagerParam : GUIManager where TDataParam : IUIDataHolder` is a generic abstract class in C#
    that extends `MonoBehaviour`. */
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

        
      /// <summary>
      /// The AssignManager function assigns a GUIManager to a parameter of type TManagerParam.
      /// </summary>
      /// <param name="GUIManager">In the provided code snippet, the `AssignManager` method takes a
      /// parameter of type `GUIManager` and assigns it to a field `_managerParam` after casting it to
      /// type `TManagerParam`.</param>
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

    public class HolderCraftingInitializator<TManagerParam> : IInitializable where TManagerParam : GUIManager
    {
        private readonly TManagerParam _managerParam;
        private readonly CraftingHolder _dataHolderParam;

        public HolderCraftingInitializator(TManagerParam managerParam, CraftingHolder dataParam)
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
