using System.Linq;
using UnityEngine;

namespace Code.Gameplay.Common.Extension
{
    public static class NextTilePositionForFalling
    {
        public static Vector2Int GetNextEmptyRow(Contexts contexts, Vector2Int position)
        {
            position.y -= 1;
            if (position.y <= 13
                && ((TileUtilsExtensions.GetTilesInCell(position).Count == 1
                     && TileUtilsExtensions.GetBottomTileByPosition(position) is not null
                     && !TileUtilsExtensions.GetBottomTileByPosition(position).hasTileForPowerUpGeneration
                     /*&& !TileUtilsExtensions.GetBottomTileByPosition(position).hasToBeInsideGenerated*/)
                    || CheckBottomTile(position)/* || CheckBottomTileForProcessedFalling(position)*/)
               )
            {
                return position;
            }
            // else if (position.y <= 13
            //          && ((TileUtilsExtensions.GetTilesInCell(position).Count == 1
            //               && TileUtilsExtensions.GetTilesInCell(position).Count > 0
            //               && TileUtilsExtensions.GetBottomTileByPosition(position) is not null
            //               /*&& !TileUtilsExtensions.GetBottomTileByPosition(position).hasToBeInsideGenerated*/)
            //              || CheckBottomTile(position))
            //         )
            // {
            //     return new Vector2Int(position.x, position.y + 1);
            // }

            position.x -= 1;

            if (position.x <= 13
                && (TileUtilsExtensions.GetTilesInCell(position).Count == 1
                    && !CheckSpawnTile(new Vector2Int(position.x, position.y + 1))
                    && TileUtilsExtensions.GetBottomTileByPosition(position) is not null
                    && !TileUtilsExtensions.GetBottomTileByPosition(position).hasTileForPowerUpGeneration)
                || CheckBottomTile(position))
            {
                if (GetTopEmptyRow(contexts, position))
                {
                    return position;
                }
            }

            position.x += 2;

            if (position.x <= 13
                && (TileUtilsExtensions.GetTilesInCell(position).Count == 1
                    && !CheckSpawnTile(new Vector2Int(position.x, position.y + 1))
                    && TileUtilsExtensions.GetBottomTileByPosition(position) is not null
                    && !TileUtilsExtensions.GetBottomTileByPosition(position).hasTileForPowerUpGeneration)
                || CheckBottomTile(position))
            {
                if (GetTopEmptyRow(contexts, position))
                {

                    return position;
                }
            }

            position.x -= 1;
            position.y += 1;

            return position;
        }

        private static bool CheckBottomTile(Vector2Int position)
        {
            GameEntity entity = TileUtilsExtensions.GetTopTileByPosition(position);

            return TileUtilsExtensions.GetTilesInCell(position).Count > 1 &&
                   (!entity.isMovable &&
                   !entity.isNotMovable || 
                   entity.isTileFallableSurface ||
                   entity.isGoalMoving);
        }

        private static bool CheckSpawnTile(Vector2Int position)
        {
            var tilesInCell = TileUtilsExtensions.GetTilesInCell(position);
            return tilesInCell.Any(tile => tile.isTileSpawner);
        }

        private static bool GetTopEmptyRow(Contexts contexts, Vector2Int position)
        {
            if (position == new Vector2Int(5, 8))
            {
            }

            var newPosition = position;
            newPosition.y += 1;

            while (newPosition.y <=
                   13 /* && contexts.game.GetTileWithPosition(position) != null && contexts.game.GetTileContentWithPosition(position) == null*/
                  )
            {
                GameEntity tileByPosition = TileUtilsExtensions.GetTopTileByPosition(newPosition);

                if (TileUtilsExtensions.GetTilesInCell(newPosition).Count > 1)
                {
                    if (tileByPosition.isMovable/* || tileByPosition.isToBeActivated ||
                        tileByPosition.isToBeDirectlyInteracted*/ || tileByPosition.hasTileForPowerUpGeneration)
                    {
                        return false;
                    }

                    if (TileUtilsExtensions.GetTilesInCell(newPosition).Any(x => x.isTileSpawner))
                    {
                        return false;
                    }

                    return true;
                }

                if (TileUtilsExtensions.GetTilesInCell(newPosition).Count == 0)
                {
                    return true;
                }

                if (TileUtilsExtensions.GetTilesInCell(newPosition).Count == 1)
                {
                    if (TileUtilsExtensions.GetTilesInCell(newPosition).Any(x => x.isTileSpawner))
                    {
                        return false;
                    }
                    
                    if (tileByPosition.hasTileForPowerUpGeneration)
                    {
                        return true;
                    }

                    newPosition.y += 1;
                }
            }

            return true;
        }
    }
}