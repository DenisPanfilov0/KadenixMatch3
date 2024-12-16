using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class BombAndRocketInteractionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public BombAndRocketInteractionSystem(GameContext game)
        {
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpBombAndRocket,
                    GameMatcher.ActiveInteraction)
                .NoneOf(GameMatcher.InteractionDelay, GameMatcher.AnimationProcess));
        }

        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                // tileInteraction.isActiveInteraction = false;

                List<GameEntity> tiles = new();

                Vector2Int centerPosition = tileInteraction.BoardPosition;

                List<int> tilesXPosition = new List<int> { centerPosition.x };
                List<int> tilesYPosition = new List<int> { centerPosition.y };

                if (centerPosition.x - 1 >= 0)
                {
                    tilesXPosition.Add(centerPosition.x - 1);
                }

                if (centerPosition.x + 1 < 13)
                {
                    tilesXPosition.Add(centerPosition.x + 1);
                }

                if (centerPosition.y - 1 >= 0)
                {
                    tilesYPosition.Add(centerPosition.y - 1);
                }

                if (centerPosition.y + 1 <= 13)
                {
                    tilesYPosition.Add(centerPosition.y + 1);
                }

                for (var x = 0; x <= 13; x++)
                {
                    for (var y = 0; y <= 13; y++)
                    {
                        Vector2Int position = new Vector2Int(x, y);

                        var tileEntity = TileUtilsExtensions.GetTopTileByPosition(position);
                        if (tileEntity != null && tileEntity.TileType != TileTypeId.powerUpMagicBall
                            && !TileUtilsExtensions.GetTilesInCell(position).Any(x => x.isTileSpawner) 
                            && (tilesXPosition.Contains(x) || tilesYPosition.Contains(y))
                            && !tileEntity.isActiveInteraction && !tileEntity.isBoardTile && !tileEntity.isTileActiveProcess)
                        {
                            tiles.Add(tileEntity);
                        }
                    }
                }

                foreach (var tile in tiles)
                {
                    tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                    tile.isActiveInteraction = true;
                    tile.isGoalCheck = true;
                }

                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                tileInteraction.isGoalCheck = true;
                tileInteraction.isPowerUpBombAndBomb = false;
                tileInteraction.isActiveInteraction = false;
                tiles.Clear();
            }
        }
    }
}