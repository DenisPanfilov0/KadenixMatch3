using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
    public class UpdateTransformPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateTransformPositionSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform)
                .NoneOf(GameMatcher.DestructedProcess, GameMatcher.AnimationProcess, GameMatcher.FindMatchesProcess));
        }
    
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                mover.Transform.position = mover.WorldPosition;
            }
        }
    }
}