using Code.Gameplay;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
  public class Match3EnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private Match3Feature _match3Feature;

    public Match3EnterState(
      IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public void Enter()
    {
      _stateMachine.Enter<Match3LoopState>();
    }

    public void Exit()
    {
      
    }
  }
}