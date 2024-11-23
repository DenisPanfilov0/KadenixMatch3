using System.Collections;
using Code.Gameplay;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
  public class Match3LoopState : IState, IUpdateable
  {
    private readonly ISystemFactory _systems;
    private Match3Feature _match3Feature;
    private readonly GameContext _gameContext;

    public Match3LoopState(ISystemFactory systems, GameContext gameContext)
    {
      _systems = systems;
      _gameContext = gameContext;
    }
    
    public void Enter()
    {
      _match3Feature = _systems.Create<Match3Feature>();
      _match3Feature.Initialize();
    }

    public void Update()
    {
      _match3Feature.Execute();
      _match3Feature.Cleanup();
    }

    public void Exit()
    {
      _match3Feature.DeactivateReactiveSystems();
      _match3Feature.ClearReactiveSystems();

      DestructEntities();
      
      _match3Feature.Cleanup();
      _match3Feature.TearDown();
      _match3Feature = null;
    }

    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities()) 
        entity.isDestructed = true;
    }
  }
}