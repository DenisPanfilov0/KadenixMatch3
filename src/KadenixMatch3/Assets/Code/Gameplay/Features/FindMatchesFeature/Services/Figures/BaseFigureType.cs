using System.Linq;
using Code.Gameplay.Common.Extension;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Services.Figures
{
    public class BaseFigureType
    {
        public static bool TryGetNeighbor(GameEntity tile, Vector2Int direction, out GameEntity neighbor)
        {
            neighbor = null;
            
            var tilesInCell = TileUtilsExtensions.GetTilesInCell(tile.BoardPosition + direction);
            if (tilesInCell.Any(e => e.isTileSpawner))
            {
                neighbor = null;
                return false;
            }
            
            var pos = tile.BoardPosition + direction;

            if (tilesInCell != null && tilesInCell.Count > 0)
            {
                foreach (var entity in tilesInCell)
                {
                    if (entity is not null && entity.isMovable)
                    {
                        neighbor = entity;
                    }
                    else if(entity is not null && entity.isTransparentToMatch)
                    {
                        continue;
                    }
                }
            }

            neighbor = TileUtilsExtensions.GetTopEntity(tilesInCell);
            return neighbor != null;
        }

        public static bool AreContentTypesSame(params GameEntity[] tiles)
        {
            var contentType = tiles[0].TileType;
            foreach (var tile in tiles)
            {
                var tilesInCell = TileUtilsExtensions.GetTilesInCell(tile.BoardPosition);
                var entitiesInCell = tilesInCell.Where(entity => entity.hasPositionInCoverageQueue)
                    .OrderByDescending(entity => entity.PositionInCoverageQueue)
                    .ToHashSet();
                   
                foreach (var entity in entitiesInCell)
                {
                    if (entity is not null && entity.isMovable && !entity.isProcessedFalling && (!entity.isTileSwipeProcessed || (entity.isTileSwipeProcessed && entity.isTileForCheckedMatch)))
                    {
                        if (entity.TileType != contentType)
                        {
                            return false;
                        }

                        break;
                    }
                    // else
                    // {
                    //     return false;
                    // }

                    if(entity is not null && entity.isTransparentToMatch)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}