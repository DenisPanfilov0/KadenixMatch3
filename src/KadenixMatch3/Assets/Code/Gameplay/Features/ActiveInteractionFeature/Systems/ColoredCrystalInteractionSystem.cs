using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.GoalsCounting.UI;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class ColoredCrystalInteractionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);
        private IGoalsUIService _goalsUIService;
        private List<GameEntity> _tiles = new();

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
                
                GameEntity nextEntity = TileUtilsExtensions.GetNextTopTileByPosition(tileInteraction.BoardPosition, tileInteraction.PositionInCoverageQueue);
                
                _tiles.Add(TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(tileInteraction.BoardPosition.x + 1,
                    tileInteraction.BoardPosition.y)));
                _tiles.Add(TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(tileInteraction.BoardPosition.x - 1,
                    tileInteraction.BoardPosition.y)));
                _tiles.Add(TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(tileInteraction.BoardPosition.x,
                    tileInteraction.BoardPosition.y + 1)));
                _tiles.Add(TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(tileInteraction.BoardPosition.x,
                    tileInteraction.BoardPosition.y - 1)));
                
                if (nextEntity != null && !nextEntity.isBoardTile && nextEntity != tileInteraction && !nextEntity.isTileActiveProcess)
                {
                    nextEntity.ReplaceDamageReceived(nextEntity.DamageReceived + 1);
                    nextEntity.isActiveInteraction = true;
                }

                foreach (var tile in _tiles)
                {
                    if (tile != null && !tile.isBoardTile && tile != tileInteraction && !tile.isTileActiveProcess && tile.isIndirectInteractable 
                        && (!tile.hasTilesInShape || tile.TilesInShape[0] != tileInteraction.TilesInShape[0]))
                    {
                        tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                        tile.isActiveInteraction = true;
                    }
                }
                
                _tiles.Clear();
            }
        }
    }
}