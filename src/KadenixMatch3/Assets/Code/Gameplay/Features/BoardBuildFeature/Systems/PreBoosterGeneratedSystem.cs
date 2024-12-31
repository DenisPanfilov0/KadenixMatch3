using System.Collections.Generic;
using Code.Common.Entity;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Meta.Feature.Shop;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public sealed class PreBoosterGeneratedSystem : IInitializeSystem
    {
        private readonly IProgressProvider _progress;
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _masksEmptyCells;

        public PreBoosterGeneratedSystem(GameContext game, IProgressProvider progress, ITileFactory tileFactory)
        {
            _progress = progress;
            _tileFactory = tileFactory;
            _masksEmptyCells = game.GetGroup(GameMatcher.MaskEmptyCellsToFill);
        }

        public void Initialize()
        {
            foreach (var maskEmptyCells in _masksEmptyCells)
            {
                var boardMask = maskEmptyCells.maskEmptyCellsToFill.Value;
                var boardMaskCopy = CopyBoardMask(boardMask);
                FillEmptyCells(boardMaskCopy, maskEmptyCells);
            }
        }

        private int[,] CopyBoardMask(int[,] originalMask)
        {
            int width = originalMask.GetLength(0);
            int height = originalMask.GetLength(1);

            int[,] newMask = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newMask[x, y] = originalMask[x, y];
                }
            }

            return newMask;
        }

        private void FillEmptyCells(int[,] boardMask, GameEntity maskEntity)
        {
            int width = boardMask.GetLength(0);
            int height = boardMask.GetLength(1);

            // Список всех пустых клеток
            var emptyCells = new List<Vector2Int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (boardMask[x, y] == 0)
                    {
                        emptyCells.Add(new Vector2Int(x, y));
                    }
                }
            }

            var random = new System.Random();

            // Заполнение бустерами случайных клеток
            foreach (var preBooster in _progress.ProgressData.ProgressModel.PreBoostersSelectedInLevel)
            {
                if (emptyCells.Count == 0)
                    break;

                int randomIndex = random.Next(emptyCells.Count);
                var randomCell = emptyCells[randomIndex];
                emptyCells.RemoveAt(randomIndex);

                int x = randomCell.x;
                int y = randomCell.y;

                GameEntity topTile = TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(x, y));

                if (preBooster == ShopItemId.PreBoosterBomb)
                {
                    _tileFactory.CreateTile(TileTypeId.powerUpBomb, new Vector3(x, y), new Vector2Int(x, y), topTile.PositionInCoverageQueue + 1);
                    _progress.ProgressData.ProgressModel.CharacterPreBoosters.PreBoosterBomb--;
                }
                else if (preBooster == ShopItemId.PreBoosterLinearLightning)
                {
                    _tileFactory.CreateTile(TileTypeId.powerUpHorizontalRocket, new Vector3(x, y), new Vector2Int(x, y), topTile.PositionInCoverageQueue + 1);
                    _progress.ProgressData.ProgressModel.CharacterPreBoosters.PreBoosterLinearLightning--;
                }
                else if (preBooster == ShopItemId.PreBoosterMagicBall)
                {
                    _tileFactory.CreateTile(TileTypeId.powerUpMagicBall, new Vector3(x, y), new Vector2Int(x, y), topTile.PositionInCoverageQueue + 1);
                    _progress.ProgressData.ProgressModel.CharacterPreBoosters.PreBoosterMagicBall--;
                }

                // Обновляем маску, заменяя значение на 6
                boardMask[x, y] = 6;
            }

            // Обновляем сущность маски
            maskEntity.ReplaceMaskEmptyCellsToFill(boardMask);

            _progress.ProgressData.ProgressModel.PreBoostersSelectedInLevel.Clear();
        }
    }
}
