using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Systems
{
    public sealed class TileContentCreateSystem : IInitializeSystem
    {
        private readonly IProgressProvider _progress;
        private readonly ITileFactory _tileFactory;

        public TileContentCreateSystem(IProgressProvider progress, ITileFactory tileFactory)
        {
            _progress = progress;
            _tileFactory = tileFactory;
        }
        
        public void Initialize()
        {
            Level lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);

            foreach (var tile in lvl.boardState)
            {
                if (tile == null || tile.tileContentType == null)
                {
                    continue;
                }
                
                if (tile == null || (tile.tileContentModifiers == null && tile.tileModifiers == null && tile.tileType == null && tile.tileContentType == null))
                    continue;
                
                GameEntity topTile = TileUtilsExtensions.GetTopTileByPosition(new Vector2Int(tile.xPos, tile.yPos));

                _tileFactory.CreateTile(TileTypeParserExtensions.TileTypeResolve(tile.tileContentType),
                    new Vector3(tile.xPos, tile.yPos), new Vector2Int(tile.xPos, tile.yPos), topTile.PositionInCoverageQueue + 1);
            }
        }
    }
}