using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TilesFallFeature.Systems
{
    public class CheckingCanFallTileSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _tiles;
        private List<GameEntity> _buffer = new (64);
        
        private float _delay = 0f;

        public CheckingCanFallTileSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _tiles = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.TileContent)
                .NoneOf(GameMatcher.StartedFalling));
        }

        public void Execute()
        {
            // if(!_tilesContext.boardState.IsActive)
            //     return;
            
            // var board = _tilesContext.boardState.BoardSize;
            
            for (var x = 0; x < 13; x++)
            {
                _delay = 0;

                for (var y = 0; y <= 13; y++)
                {
                    var position = new Vector2Int(x, y);
                    var e = TileUtilsExtensions.GetTopTileByPosition(position);
                    
                    if (e != null && e.isMovable&& !e.isCanFall && !e.isBoardTile && !e.isAnimationProcess
                        && !e.isTileSwipeProcessed && !e.isAnimationProcess && !e.isDestructedProcess && !e.isActiveInteraction)
                    {
                        MoveDown(e, position, _delay);
                    }

                }
            }
        }
        
        void MoveDown(GameEntity e, Vector2Int position, float delay)
        {
            var nextRowPos = NextTilePositionForFalling.GetNextEmptyRow(Contexts.sharedInstance, position);
            
            var entityTopTile = TileUtilsExtensions.GetTopTileByPosition(nextRowPos);

            // if (entityTopTile.isStartedFalling && entityTopTile.isProcessedFalling)
            // {
            //     return;
            // }

            if (entityTopTile == null)
            {
                return;
            }

            if (nextRowPos.y != position.y)
            {
                e.ReplacePositionInCoverageQueue(entityTopTile.PositionInCoverageQueue + 1);
                entityTopTile.ReplaceTargetId(e.Id);
                e.ReplaceBoardPosition(nextRowPos);
                e.isCanFall = true;
                e.isStartedFalling = true;
                e.isProcessedFalling = true;
                e.ReplaceFallDirection(new Vector3(nextRowPos.x, nextRowPos.y) - new Vector3(position.x, position.y));

                if (e.hasTileTweenAnimation)
                {
                    e.TileTweenAnimation.SetStartTransform();
                }
                
                e.AddFallDelay(delay);

                _delay += 0.3f;
            }
            else if (e.isProcessedFalling && !e.isCanFall)
            {
                // e.tileTweenAnimation.Value.SaveTransform();
                // e.tileTweenAnimation.Value.ScaleChange(e);
                
                e.isEndFalling = true;
                e.isProcessedFalling = false;
                e.isFindMatches = true;
                e.isStartedFalling = false;

                
                e.ReplaceFallingSpeed(4f);
                e.ReplaceAccelerationTime(0.2f);
            }
        }
    }
}