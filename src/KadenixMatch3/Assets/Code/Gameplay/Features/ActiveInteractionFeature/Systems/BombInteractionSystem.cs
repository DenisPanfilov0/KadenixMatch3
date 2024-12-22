using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class BombInteractionSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);
        
        private readonly Vector2Int[] offsets = new Vector2Int[]
        {
            new Vector2Int(-2, 0), new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1),
            new Vector2Int(0, -2), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2),
            new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1),
            new Vector2Int(2, 0)
        };

        public BombInteractionSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpBomb,
                    GameMatcher.ActiveInteraction)
                .NoneOf(GameMatcher.InteractionDelay, GameMatcher.AnimationProcess));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                tileInteraction.isActiveInteraction = false;
                
                List<GameEntity> tilesDirectInteraction = new();

                foreach (var offset in offsets)
                {
                    Vector2Int position = tileInteraction.BoardPosition + offset;
                    GameEntity entity = TileUtilsExtensions.GetTopTileByPosition(position);

                    if (entity != null && !entity.isBoardTile && entity != tileInteraction 
                        && !TileUtilsExtensions.GetTilesInCell(position).Any(x => x.isTileSpawner) && !entity.isTileActiveProcess
                        && !entity.isPowerUpMagicalBall)
                    {
                        tilesDirectInteraction.Add(entity);
                    }
                }

                foreach (var tile in tilesDirectInteraction)
                {
                    tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                    tile.isActiveInteraction = true;
                    // tile.isGoalCheck = true;
                }

                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                tileInteraction.isGoalCheck = true;
                
                tilesDirectInteraction.Clear();
            }
        }
    }
}
