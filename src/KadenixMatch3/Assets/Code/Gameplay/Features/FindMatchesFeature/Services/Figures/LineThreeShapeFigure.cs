using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Services.Figures
{
    public class LineThreeShapeFigure : BaseFigureType
    {
        private static readonly Dictionary<string, Vector2Int[]> ShapeMasks = new()
        {
            { "1", new[] { Vector2Int.left, Vector2Int.left * 2 } },
            { "2", new[] { Vector2Int.right, Vector2Int.left } },
            { "3", new[] { Vector2Int.right, Vector2Int.right * 2 } },
            
            { "4", new[] { Vector2Int.up, Vector2Int.up * 2 } },
            { "5", new[] { Vector2Int.up, Vector2Int.down } },
            { "6", new[] { Vector2Int.down, Vector2Int.down * 2 } },
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
                            {tileGroup, FigureTypeId.LineThreeShapeFigure}
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
                        {tileGroup, FigureTypeId.LineThreeShapeFigure}
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
