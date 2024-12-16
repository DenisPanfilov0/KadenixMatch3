using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class VerticalRocketInteractionSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public VerticalRocketInteractionSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpVerticalRocket,
                    GameMatcher.ActiveInteraction)
                .NoneOf(GameMatcher.InteractionDelay, GameMatcher.AnimationProcess));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                tileInteraction.isActiveInteraction = false;
                
                List<GameEntity> tilesDirectInteraction = new();
                
                for (int y = 0; y <= 13; y++)
                {
                    Vector2Int position = new Vector2Int(tileInteraction.BoardPosition.x, y);
                    GameEntity entity = TileUtilsExtensions.GetTopTileByPosition(position);
                
                    if (entity != null && !entity.isBoardTile && entity != tileInteraction
                        && !TileUtilsExtensions.GetTilesInCell(position).Any(x => x.isTileSpawner) && !entity.isTileActiveProcess)
                    {
                        tilesDirectInteraction.Add(entity);
                    }
                }

                foreach (var tile in tilesDirectInteraction)
                {
                    tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                    tile.isActiveInteraction = true;
                    tile.isGoalCheck = true;
                }

                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                
                // tileInteraction.isGoalCheck = true;
                
                tilesDirectInteraction.Clear();
            }
        }
    }
}