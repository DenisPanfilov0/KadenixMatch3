using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public sealed class TileModifierCreateSystem : IInitializeSystem
    {
        private readonly IProgressProvider _progress;
        private readonly ITileFactory _tileFactory;

        public TileModifierCreateSystem(IProgressProvider progress, ITileFactory tileFactory)
        {
            _progress = progress;
            _tileFactory = tileFactory;
        }
        
        public void Initialize()
        {
            Level lvl = _progress.ProgressData.ProgressModel.Levels[_progress.ProgressData.ProgressModel.CurrentLevel - 1];

            List<TileTypeId> spawnerTilesPool = new();

            foreach (var tile in lvl.generatableItems)
            {
                spawnerTilesPool.Add(TileTypeParserExtensions.TileTypeResolve(tile));
            }

            foreach (var tile in lvl.boardState)
            {
                foreach (var modifier in tile.tileModifiers)
                {
                    if (modifier.tileModifierType == TileTypeId.tileModifierSpawner.ToString())
                    {
                        GameEntity spawner = _tileFactory.CreateTile(TileTypeParserExtensions.TileTypeResolve(modifier.tileModifierType),
                            new Vector3(tile.xPos, tile.yPos + 1), new Vector2Int(tile.xPos, tile.yPos + 1));
                        spawner.AddSpawnerTilesPool(spawnerTilesPool);
                    }
                    else
                    {
                        GameEntity topTile = TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(tile.xPos, tile.yPos));
                        
                        _tileFactory.CreateTile(TileTypeParserExtensions.TileTypeResolve(modifier.tileModifierType),
                            new Vector3(tile.xPos, tile.yPos), new Vector2Int(tile.xPos, tile.yPos), 
                            topTile.PositionInCoverageQueue + 1, modifier.durability != 0 ? modifier.durability : 1);
                    }
                }
            }
        }
    }
}