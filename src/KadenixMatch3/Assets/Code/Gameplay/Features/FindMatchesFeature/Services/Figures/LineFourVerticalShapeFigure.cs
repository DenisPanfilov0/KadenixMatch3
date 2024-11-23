using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Services.Figures
{
    public class LineFourVerticalShapeFigure : BaseFigureType
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
                        {tileGroup, FigureTypeId.LineFourVerticalShapeFigure}
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
                TryGetNeighbor(tile, Vector2Int.down, out var bottom3) &&
                AreContentTypesSame(tile, top1, top2, bottom3)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), top1, top2, bottom3 };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.up, out var top) &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom1) &&
                TryGetNeighbor(bottom1, Vector2Int.down, out var bottom2) &&
                AreContentTypesSame(tile, top, bottom1, bottom2)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), top, bottom1, bottom2 };
            }

            return null;
        }
    }
}