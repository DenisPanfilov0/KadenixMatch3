using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using UnityEngine;

namespace Sources.TilesContext.Services.FindMatches.FigureFinder
{
    public class TreeShapeFigure : BaseFigureType
    {
        public static Dictionary<List<GameEntity>, FigureTypeId> GetFigureTiles(List<GameEntity> tiles)
        {
            foreach (var tile in tiles)
            {
                var tileGroup = TryGetShapeCenter(tile);
                if (tileGroup != null)
                {
                    foreach (var tileEntity in tileGroup)
                    {
                        tileEntity.isFindMatchesProcess = true;
                        
                        if (tileEntity.hasTilesInShape)
                        {
                            continue;
                        }
                        
                        List<int> listTilesId = new();
                        foreach (var tileId in tileGroup)
                        {
                            listTilesId.Add(tile.Id);
                        }
                        
                        tileEntity.AddTilesInShape(listTilesId);
                    }
                    
                    return new Dictionary<List<GameEntity>, FigureTypeId>
                    {
                        {tileGroup, FigureTypeId.TreeShapeFigure}
                    };
                }
            }

            return null;
        }

        private static List<GameEntity> TryGetShapeCenter(GameEntity tile)
        {
            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.left, out var left1) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right1) &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom1) &&
                TryGetNeighbor(bottom1, Vector2Int.down, out var bottomBottom) &&
                AreContentTypesSame(tile, left1, right1, bottom1, bottomBottom)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), right1, left1, bottom1, bottomBottom };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.up, out var top1) &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom2) &&
                TryGetNeighbor(tile, Vector2Int.left, out var left2) &&
                TryGetNeighbor(left2, Vector2Int.left, out var leftLeft) &&
                AreContentTypesSame(tile, top1, bottom2, left2, leftLeft)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), top1, bottom2, left2, leftLeft };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.right, out var right2) &&
                TryGetNeighbor(tile, Vector2Int.left, out var left) &&
                TryGetNeighbor(tile, Vector2Int.up, out var top2) &&
                TryGetNeighbor(top2, Vector2Int.up, out var topTop) &&
                AreContentTypesSame(tile, right2, left, top2, topTop)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), right2, left, top2, topTop };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.up, out var top) &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right) &&
                TryGetNeighbor(right, Vector2Int.right, out var rightRight) &&
                AreContentTypesSame(tile, top, bottom, right, rightRight)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), top, bottom, right, rightRight };
            }

            return null;
        }
    }
}
