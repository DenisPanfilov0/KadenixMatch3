using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
    public class TileSwipeFinishedSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private List<GameEntity> _buffer = new(4);
        private List<GameEntity> _bufferMoves = new(4);
        private readonly IGroup<GameEntity> _tilesSwiping;
        private readonly IGroup<GameEntity> _moves;

        public TileSwipeFinishedSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _tilesSwiping = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TileSwipeFinished,
                    GameMatcher.WorldPosition,
                    GameMatcher.SwipeDirection));

            _moves = game.GetGroup(GameMatcher.AllOf(GameMatcher.Moves, GameMatcher.MovesChangeAmountProcess));
        }

        public void Execute()
        {
            foreach (var tileSwiping in _tilesSwiping.GetEntities(_buffer))
            {
                if (tileSwiping.isTilePowerUp)
                {
                    if (!tileSwiping.isTileNotActivatedAfterSwap)
                    {
                        tileSwiping.isActiveInteraction = true;
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
                tileSwiping.isAnimationProcess = false;
                tileSwiping.isTileSwipeFinished = false;
            }
        }
    }
}