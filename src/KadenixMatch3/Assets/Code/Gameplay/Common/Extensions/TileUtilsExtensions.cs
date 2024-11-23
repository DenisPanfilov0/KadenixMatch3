using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay.Common.Extension
{
    public static class TileUtilsExtensions
    {
        public static GameEntity GetTopTileByPosition(Vector2Int position)
        {
            var entity = GetTopEntity(Contexts.sharedInstance.game.GetEntitiesWithBoardPosition(position));
            return entity;
        }
        
        public static GameEntity GetNextTopTileByPosition(Vector2Int position, int numberInQueue)
        {
            var tilesEntity = Contexts.sharedInstance.game.GetEntitiesWithBoardPosition(position);

            // tilesEntity.Remove(GetTopTileByPosition(position));
            
            var targetTile = tilesEntity
                .FirstOrDefault(entity => entity.hasPositionInCoverageQueue && entity.PositionInCoverageQueue == numberInQueue - 1);

            // return GetTopEntity(tilesEntity);
            return targetTile;
        }
        
        public static GameEntity GetBottomTileByPosition(Vector2Int position)
        {
            var bottomEntity = GetBottomEntity(Contexts.sharedInstance.game.GetEntitiesWithBoardPosition(position));
            return bottomEntity;
        }

        public static HashSet<GameEntity> GetTilesInCell(Vector2Int position)
        {
            HashSet<GameEntity> entitiesWithPosition = Contexts.sharedInstance.game.GetEntitiesWithBoardPosition(position);
            return entitiesWithPosition;
        }
        
        public static GameEntity GetTopEntity(HashSet<GameEntity> entitiesCollection)
        {
            var sortedEntities = entitiesCollection
                .Where(entity => entity.hasPositionInCoverageQueue/* && !entity.GenerateTile*/)
                .OrderByDescending(entity => entity.PositionInCoverageQueue)
                .ToList();


            if (sortedEntities.Count > 0)
            {            
                return sortedEntities.First();
            }

            return null;
        }
        
        public static GameEntity GetBottomEntity(HashSet<GameEntity> entitiesCollection)
        {
            var sortedEntities = entitiesCollection
                .Where(entity => entity.hasPositionInCoverageQueue/* && !entity.GenerateTile*/)
                .OrderBy(entity => entity.PositionInCoverageQueue)
                .ToList();


            if (sortedEntities.Count > 0)
            {            
                return sortedEntities.First();
            }

            return null;
        }
        
        public static bool AreTilesAdjacent(Vector2Int pos1, Vector2Int pos2)
        {
            return Mathf.Abs(pos1.x - pos2.x) == 1 && pos1.y == pos2.y ||
                   Mathf.Abs(pos1.y - pos2.y) == 1 && pos1.x == pos2.x;
        }
    }
}