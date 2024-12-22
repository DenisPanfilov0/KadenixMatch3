using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
    public class TileSwipeProcessedSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private List<GameEntity> _buffer = new(4);
        private List<GameEntity> _bufferMoves = new(4);
        private readonly IGroup<GameEntity> _tilesSwiping;
        private readonly IGroup<GameEntity> _moves;

        public TileSwipeProcessedSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _tilesSwiping = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TileSwipeProcessed,
                    GameMatcher.WorldPosition,
                    GameMatcher.SwipeDirection));

            _moves = game.GetGroup(GameMatcher.AllOf(GameMatcher.Moves, GameMatcher.MovesChangeAmountProcess));
        }
    
        public void Execute()
        {
            foreach (var tileSwiping in _tilesSwiping.GetEntities(_buffer))
            {
                var targetPosition = new Vector3(tileSwiping.BoardPosition.x, tileSwiping.BoardPosition.y);
                var currentPosition = tileSwiping.WorldPosition;
                var distance = Vector3.Distance(currentPosition, targetPosition);

                if (distance >= 0.01f)
                {
                    var step = tileSwiping.SwipeDirection * 10f * _time.DeltaTime;
                    
                    if (step.magnitude >= distance)
                    {
                        tileSwiping.ReplaceWorldPosition(targetPosition);
                    }
                    else
                    {
                        tileSwiping.ReplaceWorldPosition(currentPosition + step);
                    }
                }
                else
                {
                    if (tileSwiping.isTilePowerUp)
                    {
                        if (!tileSwiping.isTileNotActivatedAfterSwap)
                        {
                            tileSwiping.isActiveInteraction = true;
                            // tileSwiping.isGoalCheck = true;
                        }
                        else
                        {
                            tileSwiping.isTileNotActivatedAfterSwap = false;
                        }
                        
                        tileSwiping.isFirstSelectTileSwipe = false;
                        tileSwiping.isSecondSelectTileSwipe = false;
                    }
                    else
                    {
                        tileSwiping.isFindMatches = true;
                        tileSwiping.TileTweenAnimation.SaveTransform();

                        foreach (GameEntity move in _moves.GetEntities(_bufferMoves))
                        {
                            move.isMovesChangeAmountProcess = false;
                        }
                    }
                    
                    tileSwiping.isTileSwipeProcessed = false;
                }
            }
        }
    }
}