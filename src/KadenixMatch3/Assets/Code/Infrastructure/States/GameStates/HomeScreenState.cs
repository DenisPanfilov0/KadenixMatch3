using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using Code.Meta;

namespace Code.Infrastructure.States.GameStates
{
  public class HomeScreenState : IState, IUpdateable
  {
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private MetaFeature _metaFeature;

    public HomeScreenState(
      ISystemFactory systems, 
      GameContext gameContext )
    {
      _systems = systems;
      _gameContext = gameContext;
    }
    
    public void Enter()
    {
      if (_metaFeature == null)
      {
        _metaFeature = _systems.Create<MetaFeature>();
      }
      _metaFeature.Initialize();
    }

    public void Update()
    {
      _metaFeature.Execute();
      _metaFeature.Cleanup();
    }

    public void Exit()
    {
      DestructEntities();
    }
    
    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities()) 
        entity.isDestructed = true;
    }
  }
}