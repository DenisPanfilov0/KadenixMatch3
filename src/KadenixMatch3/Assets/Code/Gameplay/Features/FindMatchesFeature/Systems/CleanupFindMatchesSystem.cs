using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.FindMatchesFeature.Systems
{
    public class CleanupFindMatchesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _matchables;
        private List<GameEntity> _buffer = new(64);

        public CleanupFindMatchesSystem(GameContext game)
        {
            _matchables = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.Matchable,
                    GameMatcher.FindMatches));
        }
    
        public void Execute()
        {
            foreach (GameEntity matchable in _matchables.GetEntities(_buffer))
            {
                matchable.isFindMatches = false;
            }
        }
    }
}