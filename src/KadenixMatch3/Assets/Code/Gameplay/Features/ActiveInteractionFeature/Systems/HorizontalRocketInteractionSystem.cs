using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class HorizontalRocketInteractionSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public HorizontalRocketInteractionSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpHorizontalRocket,
                    GameMatcher.ActiveInteraction)
                .NoneOf(GameMatcher.InteractionDelay, GameMatcher.AnimationProcess));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                tileInteraction.isActiveInteraction = false;
                
                List<GameEntity> tilesDirectInteraction = new();
                
                for (int x = 0; x <= 13; x++)
                {
                    Vector2Int position = new Vector2Int(x, tileInteraction.BoardPosition.y);
                    GameEntity entity = TileUtilsExtensions.GetTopTileByPosition(position);
                
                    if (entity != null && !entity.isBoardTile && entity != tileInteraction
                        && !TileUtilsExtensions.GetTilesInCell(position).Any(x => x.isTileSpawner) && !entity.isTileActiveProcess)
                    {
                        tilesDirectInteraction.Add(entity);
                    }
                }

                // if (tileInteraction.BoardPosition.x + tileInteraction.InteractionStep > 13 &&
                //     tileInteraction.BoardPosition.x - tileInteraction.InteractionStep < 0)
                // {
                //     tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                //     break;
                // }
                //
                // tilesDirectInteraction.Add(
                //     TileUtilsExtensions
                //         .GetTopTileByPosition(new Vector2Int(tileInteraction.BoardPosition.x + tileInteraction.InteractionStep, tileInteraction.BoardPosition.y)));
                //
                // tilesDirectInteraction.Add(
                //     TileUtilsExtensions
                //         .GetTopTileByPosition(new Vector2Int(tileInteraction.BoardPosition.x - tileInteraction.InteractionStep, tileInteraction.BoardPosition.y)));



                foreach (var tile in tilesDirectInteraction)
                {
                    // if (tile != null && !tile.isBoardTile && tile != tileInteraction)
                    // {
                    //     tile.isActiveInteraction = true;
                    // }
                    
                    tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                    tile.isActiveInteraction = true;
                    tile.isGoalCheck = true;
                    // tile.isDestructedProcess = true;
                    // tile.TileTweenAnimation.TilesOnDestroy(tile);
                }

                // tileInteraction.AddInteractionDelay(0.08f);
                
                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                tileInteraction.isGoalCheck = true;
                
                tilesDirectInteraction.Clear();
            }
        }
    }
}