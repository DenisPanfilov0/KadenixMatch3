using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Services.Figures
{
    public class LineThreeShapeFigure : BaseFigureType
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
                        {tileGroup, FigureTypeId.LineThreeShapeFigure}
                    };
                }
            }

            return null;
        }

        private static List<GameEntity> TryGetShapeCenter(GameEntity tile)
        {
            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.left, out var left) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right) &&
                AreContentTypesSame(tile, left, right)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), left, right };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.down, out var bottom) &&
                TryGetNeighbor(tile, Vector2Int.up, out var top) &&
                AreContentTypesSame(tile, bottom, top)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), bottom, top };
            }

            return null;
        }

        // private static bool TryGetNeighbor(GameEntity tile, Vector2Int direction, out GameEntity neighbor)
        // {
        //     neighbor = null;
        //     
        //     var tilesInCell = TileUtilsExtensions.GetTilesInCell(tile.BoardPosition + direction);
        //     if (tilesInCell.Any(e => e.isTileSpawner))
        //     {
        //         neighbor = null;
        //         return false;
        //     }
        //     
        //     var pos = tile.BoardPosition + direction;
        //
        //     if (tilesInCell != null && tilesInCell.Count > 0)
        //     {
        //         foreach (var entity in tilesInCell)
        //         {
        //             if (entity is not null && entity.isMovable)
        //             {
        //                 neighbor = entity;
        //             }
        //             // else if(entity is not null && entity.isMatchableUnder)
        //             // {
        //             //     continue;
        //             // }
        //         }
        //     }
        //
        //     neighbor = TileUtilsExtensions.GetTopEntity(tilesInCell);
        //     return neighbor != null;
        // }
        //
        // private static bool AreContentTypesSame(params GameEntity[] tiles)
        // {
        //     var contentType = tiles[0].TileType;
        //     foreach (var tile in tiles)
        //     {
        //         var tilesInCell = TileUtilsExtensions.GetTilesInCell(tile.BoardPosition);
        //         var entitiesInCell = tilesInCell.Where(entity => entity.hasPositionInCoverageQueue)
        //             .OrderByDescending(entity => entity.PositionInCoverageQueue)
        //             .ToHashSet();
        //            
        //         foreach (var entity in entitiesInCell)
        //         {
        //             if (entity is not null && entity.isMovable && !entity.isProcessedFalling && !entity.isTileSwipeProcessed)
        //             {
        //                 if (entity.TileType != contentType)
        //                 {
        //                     return false;
        //                 }
        //
        //                 break;
        //             }
        //             else
        //             {
        //                 return false;
        //             }
        //
        //             // if(entity is not null && entity.isMatchableUnder)
        //             // {
        //             //     continue;
        //             // }
        //             // else
        //             // {
        //             //     return false;
        //             // }
        //         }
        //     }
        //     return true;
        // }
    }
}
