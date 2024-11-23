using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using UnityEngine;

namespace Sources.TilesContext.Services.FindMatches.FigureFinder
{
    public class LineFourHorizontalShapeFigure : BaseFigureType
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
                        {tileGroup, FigureTypeId.LineFourHorizontalShapeFigure}
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
                TryGetNeighbor(right1, Vector2Int.right, out var right2) &&
                AreContentTypesSame(tile, left1, right1, right2)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), left1, right1, right2 };
            }

            if (
                tile.hasBoardPosition &&
                TryGetNeighbor(tile, Vector2Int.left, out var left) &&
                TryGetNeighbor(left, Vector2Int.left, out var left2) &&
                TryGetNeighbor(tile, Vector2Int.right, out var right) &&
                AreContentTypesSame(tile, left, left2, right)
            )
            {
                return new List<GameEntity> { TileUtilsExtensions.GetTopTileByPosition(tile.BoardPosition), left, left2, right };
            }

            return null;
        }

        // private static bool TryGetNeighbor(GameEntity tile, Vector2Int direction, out GameEntity neighbor)
        // {
        //     neighbor = null;
        //     
        //     var tilesInCell = GetEntityUtils.GetTilesInCell(tile.position.Value + direction);
        //     if (tilesInCell.Any(e => e.GenerateTile))
        //     {
        //         neighbor = null;
        //         return false;
        //     }
        //     
        //     var pos = tile.position.Value + direction;
        //
        //     if (tilesInCell != null && tilesInCell.Count > 0)
        //     {
        //         foreach (var entity in tilesInCell)
        //         {
        //             if (entity is not null && entity.IsMovable)
        //             {
        //                 neighbor = entity;
        //             }
        //             else if(entity is not null && entity.isMatchableUnder)
        //             {
        //                 continue;
        //             }
        //         }
        //     }
        //
        //     neighbor = GetEntityUtils.GetTopEntity(tilesInCell);
        //     return neighbor != null;
        // }
        //
        // private static bool AreContentTypesSame(params TilesEntity[] tiles)
        // {
        //     var contentType = tiles[0].tileType.Value;
        //     foreach (var tile in tiles)
        //     {
        //         var tilesInCell = GetEntityUtils.GetTilesInCell(tile.position.Value);
        //         var entitiesInCell = tilesInCell.Where(entity => entity.hasCoveredByTargetId)
        //             .OrderByDescending(entity => entity.coveredByTargetId.PositionInCoverageQueue)
        //             .ToHashSet();
        //            
        //         foreach (var entity in entitiesInCell)
        //         {
        //             if (entity is not null && entity.IsMovable)
        //             {
        //                 if (entity.tileType.Value != contentType)
        //                 {
        //                     return false;
        //                 }
        //
        //                 break;
        //             }
        //
        //             if(entity is not null && entity.isMatchableUnder)
        //             {
        //                 continue;
        //             }
        //             else
        //             {
        //                 return false;
        //             }
        //         }
        //     }
        //     return true;
        // }
    }
}
