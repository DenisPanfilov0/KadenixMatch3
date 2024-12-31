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
                    GameMatcher.SwipeDirection)
                .NoneOf(GameMatcher.AnimationProcess, GameMatcher.TileSwipeFinished));

            _moves = game.GetGroup(GameMatcher.AllOf(GameMatcher.Moves, GameMatcher.MovesChangeAmountProcess));
        }
    
        public void Execute()
        {
            foreach (var tileSwiping in _tilesSwiping.GetEntities(_buffer))
            {
                tileSwiping.ReplaceWorldPosition(new Vector3(tileSwiping.BoardPosition.x, tileSwiping.BoardPosition.y));
                tileSwiping.BaseTileAnimation.TileSwap(tileSwiping, tileSwiping.SwipeDirection);
                tileSwiping.isAnimationProcess = true;
            }
        }
    }
}