using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.FindMatchesFeature.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Systems
{
    public class IdenticalTilesSearchSystem : IExecuteSystem
    {
        private readonly IFindMatchesService _findMatchesService;
        private readonly IGroup<GameEntity> _matchables;
        private List<GameEntity> _buffer = new(64);

        public IdenticalTilesSearchSystem(GameContext game, IFindMatchesService findMatchesService)
        {
            _findMatchesService = findMatchesService;
            _matchables = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.FindMatches));
        }
    
        public void Execute()
        {
            foreach (GameEntity matchable in _matchables.GetEntities(_buffer))
            {
                if (matchable == null || !matchable.isFindMatches)
                {
                    continue;
                }
                
                List<GameEntity> tiles = _findMatchesService.FindIdenticalTiles(matchable);

                if (tiles != null && tiles.Count <= 2)
                {
                    foreach (var tile in tiles)
                    {
                        if (tile.isFirstSelectTileSwipe || tile.isSecondSelectTileSwipe)
                        {
                            tile.isFirstSelectTileSwipe = false;
                            tile.isSecondSelectTileSwipe = false;
                        }
                    }
                    
                    continue;
                }

                GameEntity mainTile = null;

                foreach (var tile in tiles)
                {
                    if (tile.isFirstSelectTileSwipe || tile.isSecondSelectTileSwipe)
                    {
                        mainTile = tile;

                        tile.isFirstSelectTileSwipe = false;
                        tile.isSecondSelectTileSwipe = false;
                        
                        continue;
                    }
                }

                if (mainTile == null)
                {
                    mainTile = tiles.Where(x => x.isFindMatches)
                        .OrderBy(_ => Guid.NewGuid())
                        .FirstOrDefault();
                }

                if (tiles.Count >= 3 && !mainTile.isTileGroupConvergeForBooster)
                {
                    List<Vector2Int> allTilePosition = new();
                    
                    foreach (var tilePosition in tiles)
                    {
                        allTilePosition.Add(tilePosition.BoardPosition);
                    }

                    foreach (var tile in tiles)
                    {
                        tile.isFindMatches = false;
                    }
                    
                    mainTile.ReplaceIdenticalTilesForMatche(allTilePosition);
                }
            }
        }
    }
}