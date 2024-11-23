using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature.Systems
{
    public class TileGroupConvergeForBoosterSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _matchables;
        private List<GameEntity> _buffer = new(64);

        public TileGroupConvergeForBoosterSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _matchables = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.Matchable,
                    GameMatcher.IdenticalTilesForMatche,
                    GameMatcher.TileGroupConvergeForBooster)
                .NoneOf(GameMatcher.PowerUpGenerated));
        }
    
        public void Execute()
        {
            foreach (GameEntity matchable in _matchables.GetEntities(_buffer))
            {
                foreach (var tilePosition in matchable.IdenticalTilesForMatche)
                {
                    GameEntity tile = TileUtilsExtensions.GetTopTileByPosition(tilePosition);
                    
                    if (tile == matchable && !tile.hasMovedForCenterBooster)
                    {
                        continue;
                    }
                    
                    if (!tile.isAnimationProcess && tile.hasMovedForCenterBooster)
                    {
                        tile.RemoveMovedForCenterBooster();
                        // tile.isDestructedProcess = true;
                    }
                }

                if (!matchable.IdenticalTilesForMatche.Any(x => TileUtilsExtensions.GetTopTileByPosition(x).hasMovedForCenterBooster))
                {
                    matchable.isPowerUpGenerated = true;
                }
            }
        }
    }
}