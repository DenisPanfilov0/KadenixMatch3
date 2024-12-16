using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services.Figures;
using Sources.TilesContext.Services.FindMatches.FigureFinder;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Services
{
    public class FindMatchesService : IFindMatchesService
    {
        public Dictionary<List<GameEntity>, FigureTypeId> GetMatches(GameEntity entity)
        {
            var matches = entity.IdenticalTilesForMatche;
            
            List<GameEntity> allTilePosition = new();
                    
            foreach (var tilePosition in matches)
            {
                allTilePosition.Add(TileUtilsExtensions.GetTopTileByPosition(tilePosition));
            }

            if (matches.Count >= 3)
            {
                var tilesEntities = GetFigure(allTilePosition);

                if (tilesEntities is not null /*&& !tilesEntities.Any(x => x.Key.Any(y => y.isTilePowerUp))*/)
                {
                    // foreach (var figure in tilesEntities)
                    // {
                    //     entity.AddPowerUpFigureType(figure.Value);
                    // }

                    // foreach (var entities in tilesEntities.Keys)
                    // {
                    //     if (entities.All(x => !x.hasPowerUpFigureType))
                    //     {
                    //         entity.ReplacePowerUpFigureType(tilesEntities.First().Value);
                    //         return tilesEntities;
                    //     }
                    //
                    //     // entities.FirstOrDefault(x => !x.hasPowerUpFigureType);
                    //     // {
                    //         // entity.ReplacePowerUpFigureType(tilesEntities.First().Value);
                    //         // return tilesEntities;
                    //     // }
                    // }
                    

                    entity.ReplacePowerUpFigureType(tilesEntities.First().Value);
                    return tilesEntities;
                }

                // return matches;
            }

            return null;
        }

        public List<GameEntity> FindIdenticalTiles(GameEntity startTile)
        {
            var matches = new List<GameEntity>();
            var toVisit = new Stack<GameEntity>();
            var visited = new HashSet<GameEntity>();

            toVisit.Push(startTile);

            TileTypeId startContentType;
                
            if (startTile.hasTileType)
            {
                startContentType = startTile.TileType;
            }
            else
            {
                return null;
            }

            while (toVisit.Count > 0)
            {
                var currentTile = toVisit.Pop();
                if (visited.Contains(currentTile))
                {
                    continue;
                }

                if (!currentTile.hasBoardPosition)
                {
                    continue;
                }

                var tilesInCell = TileUtilsExtensions.GetTilesInCell(currentTile.BoardPosition);
                if ((currentTile.isTileSpawner || tilesInCell.Any(tile => tile.isTileSpawner)) && 
                    !currentTile.isProcessedFalling && !currentTile.isMatchable && !currentTile.isCanFall
                    && !currentTile.hasMovedForCenterBooster && !currentTile.isAnimationProcess && !currentTile.isDestructedProcess
                    && !currentTile.hasTilesInShape && !currentTile.isFindMatchesProcess && !currentTile.isTileSwipeProcessed 
                    && !currentTile.isDestructed && !currentTile.isSelectTileResearchMatches)
                {
                    continue;
                }

                visited.Add(currentTile);
                matches.Add(currentTile);
                // currentTile.isFindMatchesProcess = true;

                var neighbors = GetNeighbors(currentTile);
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor) &&
                        neighbor.isMovable &&
                        neighbor.tileType.Value == startContentType
                        // && !neighbor.isFindMatchesProcess
                        // && !neighbor.isDestructedProcess
                        // && !neighbor.isAnimationProcess
                        // && !neighbor.isTileSwipeProcessed
                        // && !neighbor.hasMovedForCenterBooster
                        // && !neighbor.hasTilesInShape
                        // && !neighbor.isProcessedFalling
                        // && !neighbor.isDestructed
                        // && !neighbor.isSelectTileResearchMatches
                        && !TileUtilsExtensions.GetTilesInCell(neighbor.BoardPosition).Any(e => e.isTileSpawner))
                    {
                        toVisit.Push(neighbor);
                    }
                }
            }

            foreach (var tile in matches)
            {
                // tile.isSelectTileResearchMatches = true;
                // tile.isFindMatchesProcess = true;
            }

            return matches;
        }

        private static List<GameEntity> GetNeighbors(GameEntity tile)
        {
            var neighbors = new List<GameEntity>();
            var pos = tile.BoardPosition;

            var possiblePositions = new List<Vector2Int>
            {
                new Vector2Int(pos.x - 1, pos.y),
                new Vector2Int(pos.x + 1, pos.y),
                new Vector2Int(pos.x, pos.y - 1),
                new Vector2Int(pos.x, pos.y + 1)
            };

            foreach (var p in possiblePositions)
            {
                var tilesInCell = TileUtilsExtensions.GetTilesInCell(p);
                if (tilesInCell.Any(e => e.isTileSpawner))
                {
                    continue;
                }

                var neighbor = Contexts.sharedInstance.game.GetEntitiesWithBoardPosition(p);

                if (neighbor != null)
                {
                    foreach (var entity in tilesInCell)
                    {
                        if (entity is not null && !entity.isFindMatchesProcess && entity.isMovable 
                            /*&& !entity.isFindMatchesProcess
                            && !entity.isDestructedProcess
                            && !entity.isAnimationProcess
                            && !entity.isTileSwipeProcessed
                            && !entity.hasMovedForCenterBooster
                            && !entity.hasTilesInShape
                            && !entity.isProcessedFalling
                            && !entity.isSelectTileResearchMatches
                            && !entity.isDestructed*/)
                        {
                            neighbors.Add(entity);
                            break;
                        }
                        else if(entity is not null && entity.isTransparentToMatch)
                        {
                            continue;
                        }
                    }
                }
            }

            return neighbors;
        }
        
        public static Dictionary<List<GameEntity>, FigureTypeId> GetFigure(List<GameEntity> entities)
        {
            List<GameEntity> tiles = new();

            foreach (var entity in entities)
            {
                if (!entity.hasBoardPosition)
                {
                    continue;
                }
                var tilesInCell = TileUtilsExtensions.GetTilesInCell(entity.BoardPosition);
                // if (tilesInCell.Any(tile => tile.GenerateTile))
                // {
                //     continue;
                // }
                 
                // if (!entity.GenerateTile && !entity.isProcessedFalling && !entity.Fell)
                // {
                    tiles.Add(entity);
                // }
            }
             
            Dictionary<List<GameEntity>, FigureTypeId> gameEntities = null;

            var figureCheckers = new List<System.Func<List<GameEntity>, Dictionary<List<GameEntity>, FigureTypeId>>>
            {
                LineFiveShapeFigure.GetFigureTiles,
                
                CornerShapeFigure.GetFigureTiles,
                TreeShapeFigure.GetFigureTiles,
                
                LineFourVerticalShapeFigure.GetFigureTiles,
                LineFourHorizontalShapeFigure.GetFigureTiles,
                
                
                LineThreeShapeFigure.GetFigureTiles,
                
                // SquareShapeFigure.GetFigureTiles,
            };

            foreach (var checker in figureCheckers)
            {
                gameEntities = checker(tiles);
                if (gameEntities != null)
                {
                    break;
                }
            }

            if (gameEntities != null)
            {
                return gameEntities;
            }

            return null;
        }
    }
}