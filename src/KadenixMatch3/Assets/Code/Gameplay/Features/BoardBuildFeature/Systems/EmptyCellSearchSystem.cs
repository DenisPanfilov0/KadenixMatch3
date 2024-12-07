using System.Collections.Generic;
using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.Common.Extension;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public sealed class EmptyCellSearchSystem : IInitializeSystem
    {
        private const int BoardWidth = 11; // Ширина доски
        private const int BoardHeight = 11; // Высота доски
        private int[,] _boardMask; // Маска доски
        
        public EmptyCellSearchSystem()
        {
            // Инициализация маски доски значениями -1 (по умолчанию)
            _boardMask = new int[BoardWidth, BoardHeight];
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    _boardMask[x, y] = -1; // По умолчанию, клетка вне игровой области
                }
            }
        }
        
        public void Initialize()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    var position = new Vector2Int(x, y);
                    var tileEntities = TileUtilsExtensions.GetTilesInCell(position);

                    if (tileEntities == null || tileEntities.Count == 0 || tileEntities.All(e => e.TileType == TileTypeId.tileModifierSpawner))
                    {
                        // Если клетка вообще отсутствует, помечаем её как -1
                        _boardMask[x, y] = -1;
                    }
                    else if (tileEntities.All(e => !e.isTileContent && !e.isTileContentModifier))
                    {
                        // Если в клетке нет контента, помечаем её как "0"
                        _boardMask[x, y] = 0;
                    }
                    else if (tileEntities.Any(e => e.isTileContent))
                    {
                        // Если есть контент, ищем его тип
                        GameEntity entity = tileEntities.First(e => e.isTileContent);

                        if (_tileMaskType.TryGetValue(entity.TileType, out int maskValue))
                        {
                            // Присваиваем значение из словаря
                            _boardMask[x, y] = maskValue;
                        }
                        else
                        {
                            // Если тип не найден в словаре, оставляем клетку пустой
                            _boardMask[x, y] = 5;
                        }
                    }
                }
            }

            CreateEntity.Empty()
                .AddMaskEmptyCellsToFill(_boardMask);

            // Вывод маски в консоль для отладки
            // PrintBoardMask();
        }

        private void PrintBoardMask()
        {
            for (int y = BoardHeight - 1; y >= 0; y--)
            {
                string row = "";
                for (int x = 0; x < BoardWidth; x++)
                {
                    row += $"{_boardMask[x, y],2} ";
                }
                Debug.Log(row);
            }
        }

        private Dictionary<TileTypeId, int> _tileMaskType = new()
        {
            {TileTypeId.coloredBlue, 1},
            {TileTypeId.coloredRed, 2},
            {TileTypeId.coloredYellow, 3},
            {TileTypeId.coloredGreen, 4},
            {TileTypeId.coloredPurple, 5},
        };
    }
}