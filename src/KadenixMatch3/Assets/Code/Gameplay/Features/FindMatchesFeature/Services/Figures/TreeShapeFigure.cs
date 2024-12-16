using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using UnityEngine;

namespace Sources.TilesContext.Services.FindMatches.FigureFinder
{
    public class TreeShapeFigure : BaseFigureType
    {
        private static readonly Dictionary<string, Vector2Int[]> ShapeMasks = new()
        {
            { "1", new[] { Vector2Int.right, Vector2Int.right * 2, Vector2Int.right + Vector2Int.down, Vector2Int.right + Vector2Int.down * 2 } },
            { "2", new[] { Vector2Int.left, Vector2Int.right, Vector2Int.down, Vector2Int.down * 2 } },
            { "3", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.left + Vector2Int.down, Vector2Int.left + Vector2Int.down * 2 } },
            { "4", new[] { Vector2Int.down, Vector2Int.up, Vector2Int.up + Vector2Int.left, Vector2Int.up + Vector2Int.right } },
            { "5", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.up * 2 + Vector2Int.left, Vector2Int.up * 2 + Vector2Int.right } },
            
            { "6", new[] { Vector2Int.right, Vector2Int.right * 2, Vector2Int.right * 2 + Vector2Int.up, Vector2Int.right * 2 + Vector2Int.down } },
            { "7", new[] { Vector2Int.left, Vector2Int.right, Vector2Int.right + Vector2Int.up, Vector2Int.right + Vector2Int.down } },
            { "8", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.up, Vector2Int.down } },
            { "9", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.down + Vector2Int.left, Vector2Int.down + Vector2Int.left * 2 } },
            { "10", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.up + Vector2Int.left, Vector2Int.up + Vector2Int.left * 2 } },
            
            { "11", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.down * 2 + Vector2Int.left, Vector2Int.down * 2 + Vector2Int.right } },
            { "12", new[] { Vector2Int.down, Vector2Int.up, Vector2Int.down + Vector2Int.left, Vector2Int.down + Vector2Int.right } },
            { "13", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.left, Vector2Int.right } },
            { "14", new[] { Vector2Int.right, Vector2Int.right * 2, Vector2Int.right + Vector2Int.up, Vector2Int.right + Vector2Int.up * 2 } },
            { "15", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.left + Vector2Int.up, Vector2Int.left + Vector2Int.up * 2 } },
            
            { "16", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.left * 2 + Vector2Int.up, Vector2Int.left * 2 + Vector2Int.down } },
            { "17", new[] { Vector2Int.left, Vector2Int.right, Vector2Int.left + Vector2Int.up, Vector2Int.left + Vector2Int.down } },
            { "18", new[] { Vector2Int.right, Vector2Int.right * 2, Vector2Int.up, Vector2Int.down } },
            { "19", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.down + Vector2Int.right, Vector2Int.down + Vector2Int.right * 2 } },
            { "20", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.up + Vector2Int.right, Vector2Int.up + Vector2Int.right * 2 } },
        };
        
        public static Dictionary<List<GameEntity>, FigureTypeId> GetFigureTiles(List<GameEntity> tiles)
        {
            foreach (var tile in tiles)
            {
                if (!tile.hasIdenticalTilesForMatche)
                {
                    continue;
                }
                
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
