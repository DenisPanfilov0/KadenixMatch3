using System.Collections.Generic;
using Code.Common.Entity;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public class FillEmptyCellSystem : IInitializeSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _masksEmptyCells;
        private readonly IGroup<GameEntity> _modifierMaskEmptyCells;

        private Dictionary<int, TileTypeId> _tileMaskType = new()
        {
            {1, TileTypeId.coloredBlue},
            {2, TileTypeId.coloredRed},
            {3, TileTypeId.coloredYellow},
            {4, TileTypeId.coloredGreen},
            {5, TileTypeId.coloredPurple},
        };

        public FillEmptyCellSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _masksEmptyCells = game.GetGroup(GameMatcher.MaskEmptyCellsToFill);
            _modifierMaskEmptyCells = game.GetGroup(GameMatcher.ModifierMaskEmptyCellsToFill);
        }

        public void Initialize()
        {
            foreach (var maskEmptyCells in _masksEmptyCells)
            {
                foreach (var modifierMaskEmptyCells in _modifierMaskEmptyCells)
                {
                    // Извлекаем оригинальную и модифицированную маски
                    var originalMask = maskEmptyCells.MaskEmptyCellsToFill;
                    var modifiedMask = modifierMaskEmptyCells.ModifierMaskEmptyCellsToFill;

                    // Предполагаем, что маски представлены в виде двумерных массивов int
                    for (int y = 0; y < originalMask.GetLength(1); y++)
                    {
                        for (int x = 0; x < originalMask.GetLength(0); x++)
                        {
                            if (originalMask[x, y] == 0) // Если значение в оригинальной маске равно 0
                            {
                                int tileKey = modifiedMask[x, y];

                                if (_tileMaskType.TryGetValue(tileKey, out var tileTypeId))
                                {
                                    // Получаем верхний тайл в позиции
                                    GameEntity topTile = TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(x, y));

                                    // Создаём новый тайл
                                    _tileFactory.CreateTile(
                                        tileTypeId,
                                        new Vector3(x, y),
                                        new Vector2Int(x, y),
                                        topTile.PositionInCoverageQueue + 1);
                                }
                                else
                                {
                                    Debug.LogWarning($"Не удалось найти TileTypeId для ключа {tileKey}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
