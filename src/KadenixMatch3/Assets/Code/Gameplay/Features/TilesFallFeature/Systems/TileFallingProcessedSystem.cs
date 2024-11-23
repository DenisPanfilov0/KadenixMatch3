using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TilesFallFeature.Systems
{
    public class TileFallingProcessedSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private List<GameEntity> _buffer = new(4);
        private readonly IGroup<GameEntity> _tilesFalling;

        public TileFallingProcessedSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _tilesFalling = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.FallDirection,
                    GameMatcher.ProcessedFalling)
                .NoneOf(GameMatcher.FallDelay));
        }
    
        public void Execute()
        {
            foreach (var tileFalling in _tilesFalling.GetEntities(_buffer))
            {
                var targetPosition = new Vector3(tileFalling.BoardPosition.x, tileFalling.BoardPosition.y);
                var currentPosition = tileFalling.WorldPosition;
                var distance = Vector3.Distance(currentPosition, targetPosition);

                if (distance >= 0.01f)
                {
                    var step = tileFalling.FallDirection * 7f * _time.DeltaTime;
                    
                    if (step.magnitude >= distance)
                    {
                        tileFalling.ReplaceWorldPosition(targetPosition);
                    }
                    else
                    {
                        tileFalling.ReplaceWorldPosition(currentPosition + step);
                    }
                }
                else
                {
                    tileFalling.isCanFall = false;
                    tileFalling.RemoveFallDirection();
                    tileFalling.ReplaceWorldPosition(new Vector3(tileFalling.BoardPosition.x, tileFalling.BoardPosition.y));
                }
            }
        }
    }
}