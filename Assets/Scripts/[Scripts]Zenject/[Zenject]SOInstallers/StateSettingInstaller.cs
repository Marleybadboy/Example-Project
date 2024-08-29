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

        //Container.BindInstance(new InventoryManager()).NonLazy();
        Container.Bind<InventoryManager>().AsSingle().NonLazy();

        Container.Bind<InventoryState>().FromInstance((InventoryState)_gameStateSettings.GetState(new InventoryState()));

        Container.Inject((InventoryState)_gameStateSettings.GetState(new InventoryState()));

        //Container.Bind<IInitializable>().To<InitializeStateBinder<InventoryState, InventoryManager>>().AsSingle().Lazy();
        
        Container.Bind<IInitializable>().To<ControleInjection>().AsSingle().NonLazy();

        Debug.Log($"jest {nameof(Inventory)}: {Container.HasBinding<Inventory>()}");


    }
}