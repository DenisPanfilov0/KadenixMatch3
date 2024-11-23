using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using UnityEngine;

namespace Sources.TilesContext.Services.FindMatches.FigureFinder
{
    public class LineFiveShapeFigure : BaseFigureType
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
                        {tileGroup, FigureTypeId.LineFiveShapeFigure}
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
                TryGetNeighbor(left1, Vector2Int.left, out var left2) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right1) &&
                TryGetNeighbor(right1, Vector2Int.right, out var right2) &&
                AreContentTypesSame(tile, left1, left2, right1, right2)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), left1, left2, right1, right2 };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.down, out var down1) &&
                TryGetNeighbor(down1, Vector2Int.down, out var down2) &&
                TryGetNeighbor(tile, Vector2Int.up, out var up1) &&
                TryGetNeighbor(up1, Vector2Int.up, out var up2) &&
                AreContentTypesSame(tile, down1, down2, up1, up2)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), down1, down2, up1, up2 };
            }

            return null;
        }
    }
}
