using Code.Common.EntityIndices;
using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Random;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Gameplay.Features.CountingMoves.Services;
using Code.Gameplay.Features.FindMatchesFeature.Services;
using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Features.GoalsCounting.UI;
using Code.Gameplay.Features.SettingInGame.Services;
using Code.Gameplay.Features.SkillCharacter.Services;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Identifiers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Factory;
using Code.Meta.Feature.Gold.Services;
using Code.Meta.Feature.Heart.Services;
using Code.Meta.Feature.Shop.Services;
using Code.Meta.Feature.StartLevel.Service;
using Code.Meta.Feature.StreakLevelsRewarded.Services;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
  {
    public override void InstallBindings()
    {
      BindInputService();
      BindInfrastructureServices();
      BindAssetManagementServices();
      BindCommonServices();
      BindSystemFactory();
      BindUIFactories();
      BindContexts();
      BindGameplayServices();
      BindMetaServices();
      BindUIServices();
      BindCameraProvider();
      BindGameplayFactories();
      BindEntityIndices();
      BindStateMachine();
      BindStateFactory();
      BindGameStates();
      BindProgressServices();
    }

    private void BindStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
    }

    private void BindStateFactory()
    {
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
    }

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingHomeScreenState>().AsSingle();
      Container.BindInterfacesAndSelfTo<HomeScreenState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingMatch3State>().AsSingle();
      Container.BindInterfacesAndSelfTo<Match3EnterState>().AsSingle();
      Container.BindInterfacesAndSelfTo<Match3LoopState>().AsSingle();
      Container.BindInterfacesAndSelfTo<RestartMatch3LevelState>().AsSingle();
    }

    private void BindContexts()
    {
      Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
      
      Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
      Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
      Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
    }

    private void BindCameraProvider()
    {
    }

    private void BindProgressServices()
    {
      Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
    }

    private void BindMetaServices()
    {
      Container.Bind<IStreakLevelsRewardUIService>().To<StreakLevelsRewardUIService>().AsSingle();
      Container.Bind<IStartLevelUIService>().To<StartLevelUIService>().AsSingle();
      Container.Bind<IShopItemUIService>().To<ShopItemUIService>().AsSingle();
      Container.Bind<ICharacterHeartUIService>().To<CharacterHeartUIService>().AsSingle();
      Container.Bind<ICharacterGoldUIService>().To<CharacterGoldUIService>().AsSingle();
    }

    private void BindGameplayServices()
    {
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      Container.Bind<IFindMatchesService>().To<FindMatchesService>().AsSingle();
      Container.Bind<ISkillUIService>().To<SkillUIService>().AsSingle();
      Container.Bind<IGoalsUIService>().To<GoalsUIService>().AsSingle();
      Container.Bind<IGameWinOrLoseUIService>().To<GameWinOrLoseUIService>().AsSingle();
      Container.Bind<IMovesInGameService>().To<MovesInGameService>().AsSingle();
      Container.Bind<ISettingsInGameService>().To<SettingsInGameService>().AsSingle();
    }

    private void BindGameplayFactories()
    {
      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
      Container.Bind<ITileFactory>().To<TileFactory>().AsSingle();
    }

    private void BindEntityIndices()
    {
      Container.BindInterfacesAndSelfTo<GameEntityIndices>().AsSingle();
    }

    private void BindSystemFactory()
    {
      Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
    }

    private void BindAssetManagementServices()
    {
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    }

    private void BindCommonServices()
    {
      Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
      Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
      Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void BindInputService()
    {
    }

    private void BindUIServices()
    {
      Container.Bind<IWindowService>().To<WindowService>().AsSingle();
    }

    private void BindUIFactories()
    {
      Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
    }
    
    public void Initialize()
    {
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
    }
  }
}