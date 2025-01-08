using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.ActiveStartStateTilesFeature.Systems
{
    public class GrassModifierActiveStartStateSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesActiveState;
        private List<GameEntity> _buffer = new(64);

        public GrassModifierActiveStartStateSystem(GameContext game)
        {
            _tilesActiveState = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.GrassModifier,
                    GameMatcher.InactiveStartState));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileActiveState in _tilesActiveState.GetEntities(_buffer))
            {
                if (tileActiveState.hasBaseTileAnimation)
                {
                    tileActiveState.BaseTileAnimation.DurabilityChange(tileActiveState);

                    tileActiveState.isInactiveStartState = false;
                }
            }
        }
    }
}