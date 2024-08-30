using HCC.GameState;
using HCC.GUI;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "State Setting Installer", menuName = "Installers/State Setting Installer")]
public class StateSettingInstaller : ScriptableObjectInstaller<StateSettingInstaller>
{
    [SerializeReference] private GameStateControler _gameStateSettings; 

    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromComponentInHierarchy(true).AsSingle().NonLazy();
        
        Container.Bind<GameStateControler>().FromInstance(_gameStateSettings).AsSingle();

        Container.Bind<UsefullHolder>().FromComponentInHierarchy(true).AsSingle();

        BindCrafting();

        Container.Bind<InventoryManager>().AsSingle().NonLazy();

        Container.Bind<InventoryState>().FromInstance((InventoryState)_gameStateSettings.GetState(new InventoryState()));

        Container.Inject((InventoryState)_gameStateSettings.GetState(new InventoryState()));

        Container.Bind<IInitializable>().To<ControleInjection>().AsSingle().NonLazy();

        Container.Bind<IInitializable>().To<CraftingManagerInitializator>().AsSingle().NonLazy();


    }

    private void BindCrafting() 
    {

        Container.Bind<CraftingManager>().AsSingle().NonLazy();

        Container.Bind<CraftingHolder>().FromComponentInHierarchy(true).AsCached();

        Container.Bind<IInitializable>().To<HolderCraftingInitializator<CraftingManager>>().AsSingle().NonLazy();

    }



 
}

public class CraftingManagerInitializator : IInitializable
{
    private readonly CraftingManager _craftingManager;
    private readonly InventoryManager _inventoryManager;

    public CraftingManagerInitializator(CraftingManager craftingManager, InventoryManager inventoryManager)
    {
        _craftingManager = craftingManager;
        _inventoryManager = inventoryManager;
    }

    public void Initialize()
    {
        _craftingManager.AssignInventory(_inventoryManager);
    }
}