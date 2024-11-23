using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
  public class HomeScreenState : IState, IUpdateable
  {
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;

    public HomeScreenState(
      ISystemFactory systems, 
      GameContext gameContext )
    {
      _systems = systems;
      _gameContext = gameContext;
    }
    
    public void Enter()
    {
    }

    public void Update()
    {
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