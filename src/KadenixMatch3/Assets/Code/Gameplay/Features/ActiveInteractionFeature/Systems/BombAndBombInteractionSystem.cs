using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class BombAndBombInteractionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public BombAndBombInteractionSystem(GameContext game)
        {
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpBombAndBomb,
                    GameMatcher.ActiveInteraction)
                .NoneOf(GameMatcher.InteractionDelay/*, GameMatcher.AnimationProcess*/));
        }

        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                tileInteraction.isActiveInteraction = false;

                List<GameEntity> tilesDirectInteraction = new();
                
                // Позиция текущего тайла
                var centralPosition = tileInteraction.BoardPosition;

                // Смещения для формирования паттерна
                var offsets = new List<Vector2Int>
                {
                    new Vector2Int(-3, 0), new Vector2Int(-3, -1), new Vector2Int(-3, +1), 

                    new Vector2Int(-2, -1), new Vector2Int(-2, 0), new Vector2Int(-2, 1), new Vector2Int(-2, -2), new Vector2Int(-2, +2), 

                    new Vector2Int(-1, -2), new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(-1, 2), new Vector2Int(-1, -3), new Vector2Int(-1, +3), 

                    new Vector2Int(0, -3), new Vector2Int(0, -2), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(0, 3),

                    new Vector2Int(1, -2), new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2), new Vector2Int(1, -3), new Vector2Int(1, +3), 

                    new Vector2Int(2, -1), new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, -2), new Vector2Int(2, +2), 

                    new Vector2Int(3, 0), new Vector2Int(3, +1), new Vector2Int(3, -1)
                };

                // Добавляем тайлы в tilesDirectInteraction на основе смещений
                foreach (var offset in offsets)
                {
                    var position = centralPosition + offset;
                    var tileEntity = TileUtilsExtensions.GetTopTileByPosition(position);
                    if (tileEntity != null && tileEntity.TileType != TileTypeId.powerUpMagicBall
                        && !TileUtilsExtensions.GetTilesInCell(position).Any(x => x.isTileSpawner)
                        && !tileEntity.isActiveInteraction && !tileEntity.isBoardTile && !tileEntity.isTileActiveProcess
                        && !tileEntity.isPowerUpMagicalBall)
                    {
                        tilesDirectInteraction.Add(tileEntity);
                    }
                }

                // Помечаем выбранные тайлы как активные для взаимодействия
                foreach (var tile in tilesDirectInteraction)
                {
                    tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                    tile.isActiveInteraction = true;
                    // tile.isGoalCheck = true;
                }

                // Анимация уничтожения тайла
                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                tileInteraction.isGoalCheck = true;
                tileInteraction.isPowerUpBombAndBomb = false;
                tileInteraction.isActiveInteraction = false;
                
                tilesDirectInteraction.Clear();
            }
        }
    }
}
