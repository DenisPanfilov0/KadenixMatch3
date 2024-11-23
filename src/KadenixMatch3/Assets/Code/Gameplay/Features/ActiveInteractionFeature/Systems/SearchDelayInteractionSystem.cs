using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class SearchDelayInteractionSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public SearchDelayInteractionSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.ActiveInteraction,
                    GameMatcher.InteractionDelay));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                if (tileInteraction.InteractionDelay <= 0)
                {
                    tileInteraction.RemoveInteractionDelay();
                    tileInteraction.ReplaceInteractionStep(tileInteraction.InteractionStep + 1);
                }
                else
                {
                    tileInteraction.ReplaceInteractionDelay(tileInteraction.InteractionDelay - _time.DeltaTime);
                }
            }
        }
    }
}