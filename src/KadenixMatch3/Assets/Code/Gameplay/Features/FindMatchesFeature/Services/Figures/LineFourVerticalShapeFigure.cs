using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Services.Figures
{
    public class LineFourVerticalShapeFigure : BaseFigureType
    {
        private static readonly Dictionary<string, Vector2Int[]> ShapeMasks = new()
        {
            { "1", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.down * 3 } },
            { "2", new[] { Vector2Int.up, Vector2Int.down, Vector2Int.down * 2 } },
            { "3", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.down } },
            { "4", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.up * 3 } },
        };
        
        public static Dictionary<List<GameEntity>, FigureTypeId> GetFigureTiles(List<GameEntity> tiles)
        {
            foreach (var tile in tiles)
            {
                if (!tile.hasIdenticalTilesForMatche && !tile.isTileForCheckedMatch)
                {
                    continue;
                }
                
                var tileGroup = TryGetShapeCenter(tile);
                if (tileGroup != null)
                {
                    if (tile.isTileForCheckedMatch)
                    {
                        return new Dictionary<List<GameEntity>, FigureTypeId>
                        {
                            {tileGroup, FigureTypeId.LineFourVerticalShapeFigure}
                        };
                    }
                    
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
            if (!tile.hasBoardPosition)
                return null;

            foreach (var mask in ShapeMasks.Values)
            {
                var matchingTiles = new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition) };
                var isMatch = true;

                foreach (var offset in mask)
                {
                    if (TryGetNeighbor(tile, offset, out var neighbor) && AreContentTypesSame(tile, neighbor))
                    {
                        matchingTiles.Add(neighbor);
                    }
                    else
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                    return matchingTiles;
            }

            return null;
        }
    }
}