using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public sealed class BoardTileCreateSystem : IInitializeSystem
    {
        private readonly IProgressProvider _progress;
        private readonly ITileFactory _tileFactory;

        public BoardTileCreateSystem(IProgressProvider progress, ITileFactory tileFactory)
        {
            _progress = progress;
            _tileFactory = tileFactory;
        }
        
        public void Initialize()
        {
            // Level lvl = _progress.ProgressData.ProgressModel.Levels[_progress.ProgressData.ProgressModel.CurrentLevel - 1];
            Level lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);

            foreach (var tile in lvl.boardState)
            {
                if (tile == null || (tile.tileContentModifiers == null && tile.tileModifiers == null && tile.tileType == null && tile.tileContentType == null))
                    continue;
                
                _tileFactory.CreateTile(TileTypeParserExtensions.TileTypeResolve(tile.tileType),
                    new Vector3(tile.xPos, tile.yPos), new Vector2Int(tile.xPos, tile.yPos), 0);
            }
        }
    }
}