using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using UnityEngine;

namespace Sources.TilesContext.Services.FindMatches.FigureFinder
{
    public class CornerShapeFigure : BaseFigureType
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
                        {tileGroup, FigureTypeId.CornerShapeFigure}
                    };
                }
            }
            
            return null;
        }



        private static List<GameEntity> TryGetShapeCenter(GameEntity tile)
        {
            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.up, out var top1) &&
                TryGetNeighbor(top1, Vector2Int.up, out var top2) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right1) &&
                TryGetNeighbor(right1, Vector2Int.right, out var right2) &&
                AreContentTypesSame(tile, top1, top2, right1, right2)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), top1, top2, right1, right2 };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom3) &&
                TryGetNeighbor(bottom3, Vector2Int.down, out var bottom4) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right3) &&
                TryGetNeighbor(right3, Vector2Int.right, out var right4) &&
                AreContentTypesSame(tile, bottom3, bottom4, right3, right4)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), bottom3, bottom4, right3, right4 };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom1) &&
                TryGetNeighbor(bottom1, Vector2Int.down, out var bottom2) &&
                TryGetNeighbor(tile, Vector2Int.left, out var left3) &&
                TryGetNeighbor(left3, Vector2Int.left, out var left4) &&
                AreContentTypesSame(tile, bottom1, bottom2, left3, left4)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), bottom1, bottom2, left3, left4 };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.up, out var top3) &&
                TryGetNeighbor(top3, Vector2Int.up, out var top4) &&
                TryGetNeighbor(tile, Vector2Int.left, out var left1) &&
                TryGetNeighbor(left1, Vector2Int.left, out var left2) &&
                AreContentTypesSame(tile, top3, top4, left1, left2)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), top3, top4, left1, left2 };
            }

            return null;
        }
    }
}
