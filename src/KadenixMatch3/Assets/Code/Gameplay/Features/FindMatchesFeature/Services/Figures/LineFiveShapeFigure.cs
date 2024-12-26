using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using UnityEngine;

namespace Sources.TilesContext.Services.FindMatches.FigureFinder
{
    public class LineFiveShapeFigure : BaseFigureType
    {
        private static readonly Dictionary<string, Vector2Int[]> ShapeMasks = new()
        {
            // Горизонтальные линии
            { "g11112", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.left * 3, Vector2Int.left * 4 } },
            { "g11121", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.left * 3, Vector2Int.right } },
            { "g11211", new[] { Vector2Int.left, Vector2Int.left * 2, Vector2Int.right, Vector2Int.right * 2 } },
            { "g12111", new[] { Vector2Int.right, Vector2Int.right * 2, Vector2Int.right * 3, Vector2Int.left } },
            { "g21111", new[] { Vector2Int.right, Vector2Int.right * 2, Vector2Int.right * 3, Vector2Int.right * 4 } },
            
            { "v21111", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.down * 3, Vector2Int.down * 4 } },
            { "v12111", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.down * 3, Vector2Int.up } },
            { "v11211", new[] { Vector2Int.down, Vector2Int.down * 2, Vector2Int.up, Vector2Int.up * 2 } },
            { "v11121", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.up * 3, Vector2Int.down } },
            { "v11112", new[] { Vector2Int.up, Vector2Int.up * 2, Vector2Int.up * 3, Vector2Int.up * 4 } },
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
                            {tileGroup, FigureTypeId.LineFiveShapeFigure}
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
                        { tileGroup, FigureTypeId.LineFiveShapeFigure }
                    };
                }
            }

            return null;
        }

        public static List<GameEntity> TryGetShapeCenter(GameEntity tile)
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
