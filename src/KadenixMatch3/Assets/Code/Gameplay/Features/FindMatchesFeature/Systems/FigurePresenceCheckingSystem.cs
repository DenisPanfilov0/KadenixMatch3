using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.FindMatchesFeature.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Systems
{
    public class FigurePresenceCheckingSystem : IExecuteSystem
    {
        private readonly IFindMatchesService _findMatchesService;
        private readonly IGroup<GameEntity> _matchables;
        private List<GameEntity> _buffer = new(64);

        public FigurePresenceCheckingSystem(GameContext game, IFindMatchesService findMatchesService)
        {
            _findMatchesService = findMatchesService;
            _matchables = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.Matchable,
                    GameMatcher.IdenticalTilesForMatche)
                .NoneOf(
                    GameMatcher.TileGroupConvergeForBooster,
                    GameMatcher.PowerUpGenerated));
        }
    
        public void Execute()
        {
            foreach (GameEntity matchable in _matchables.GetEntities(_buffer))
            {
                Dictionary<List<GameEntity>,FigureTypeId> matches = _findMatchesService.GetMatches(matchable);
                
                if (matches != null && matches.Count > 0)
                {
                    GameEntity centerBooster = null;
                    
                    foreach (var matchs in matches.Keys)
                    {
                        if(matchs.Count == 3)
                        {
                            foreach (var match in matchs)
                            {
                                // match.isDestructed = true;
                                // match.isAnimationProcess = true;
                                match.ReplaceDamageReceived(match.DamageReceived + 1);
                                match.isActiveInteraction = true;
                                // match.isGoalCheck = true;
                                // match.TileTweenAnimation.TilesOnDestroy(match);
                                // match.isDestructedProcess = true;
                            }
                            
                            matchable.RemoveIdenticalTilesForMatche();
                        }
                        else
                        {
                            foreach (var match in  matchs)
                            {
                                centerBooster = matchs.FirstOrDefault(x =>
                                    x.hasIdenticalTilesForMatche);
                                    
                                if (match.hasIdenticalTilesForMatche)
                                {
                                    List<Vector2Int> allTilePosition = new();
                    
                                    foreach (var tilePosition in matchs)
                                    {
                                        allTilePosition.Add(tilePosition.BoardPosition);
                                    }
                                    
                                    matchable.isTileGroupConvergeForBooster = true;
                                    matchable.ReplaceIdenticalTilesForMatche(allTilePosition);
                                }

     

                                if (match.hasTileTweenAnimation && match.isMovable)
                                {
                                    match.ReplaceMovedForCenterBooster(centerBooster.WorldPosition);
                                    match.isAnimationProcess = true;
                                    match.TileTweenAnimation.MoveForCenterBooster(match);
                                }
                                else if (!match.isMovable)
                                {
                                    match.ReplaceDamageReceived(match.DamageReceived + 1);
                                    match.isActiveInteraction = true;
                                    match.isGoalCheck = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    matchable.RemoveIdenticalTilesForMatche();
                }
            }
        }
    }
}