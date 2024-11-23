using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.GoalsCounting.UI;
using Entitas;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class ColoredCrystalInteractionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);
        private IGoalsUIService _goalsUIService;

        public ColoredCrystalInteractionSystem(GameContext game, IGoalsUIService goalsUIService)
        {
            _goalsUIService = goalsUIService;
            
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.ColoredCrystal,
                    GameMatcher.ActiveInteraction));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                tileInteraction.isActiveInteraction = false;
                tileInteraction.isGoalCheck = true;
                
                //todo(d.p.) это переносим в отдельную систему
                // _goalsUIService.ChangeGoalCount(tileInteraction.TileType, 1);

                GameEntity nextEntity = TileUtilsExtensions.GetNextTopTileByPosition(tileInteraction.BoardPosition, tileInteraction.PositionInCoverageQueue);
                if (!nextEntity.isBoardTile && nextEntity != tileInteraction && !nextEntity.isTileActiveProcess)
                {
                    nextEntity.ReplaceDamageReceived(nextEntity.DamageReceived + 1);
                    nextEntity.isActiveInteraction = true;
                }
                
                // tile
            }
        }
    }
}