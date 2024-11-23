using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class GrassModifierInteractionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public GrassModifierInteractionSystem(GameContext game)
        {
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.GrassModifier,
                    GameMatcher.ActiveInteraction));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                for (int i = 0; i < tileInteraction.DamageReceived; i++)
                {
                    if (tileInteraction.TileDurability > 1)
                    {
                        tileInteraction.ReplaceTileDurability(tileInteraction.TileDurability - 1);
                        tileInteraction.GrassModifierAnimation.DurabilityChange(tileInteraction);
                    }
                    else if (tileInteraction.TileDurability == 1)
                    {
                        tileInteraction.GrassModifierAnimation.TilesOnDestroy(tileInteraction);
                    }
                }

                tileInteraction.isActiveInteraction = false;
                tileInteraction.ReplaceDamageReceived(0);
            }
        }
    }
}