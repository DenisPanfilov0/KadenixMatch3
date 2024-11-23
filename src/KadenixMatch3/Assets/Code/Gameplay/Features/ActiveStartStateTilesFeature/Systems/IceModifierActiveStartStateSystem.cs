using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.ActiveStartStateTilesFeature.Systems
{
    public class IceModifierActiveStartStateSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesActiveState;
        private List<GameEntity> _buffer = new(64);

        public IceModifierActiveStartStateSystem(GameContext game)
        {
            _tilesActiveState = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.IceModifier,
                    GameMatcher.InactiveStartState));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileActiveState in _tilesActiveState.GetEntities(_buffer))
            {
                if (tileActiveState.hasIceModifierAnimation)
                {
                    tileActiveState.IceModifierAnimation.DurabilityChange(tileActiveState);

                    tileActiveState.isInactiveStartState = false;
                }
            }
        }
    }
}