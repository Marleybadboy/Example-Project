using HCC.GameState;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "State Setting Installer", menuName = "Installers/State Setting Installer")]
public class StateSettingInstaller : ScriptableObjectInstaller<StateSettingInstaller>
{
    [SerializeReference] private GameStateControler _gameStateSettings; 

    public override void InstallBindings()
    {
      
        Container.Bind<Inventory>().FromComponentInHierarchy(true).AsCached();
        Container.Bind<GameStateControler>().FromInstance(_gameStateSettings).AsSingle();
        Container.Bind<IInitializable>().To<ControleInjection>().AsSingle().NonLazy();


    }
}