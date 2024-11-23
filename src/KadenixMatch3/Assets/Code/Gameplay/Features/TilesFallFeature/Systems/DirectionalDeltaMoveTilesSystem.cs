using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TilesFallFeature.Systems
{
    public class DirectionalDeltaMoveTilesSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        
        private List<GameEntity> _buffer = new(64);
        private readonly IGroup<GameEntity> _tilesFalling;

        public DirectionalDeltaMoveTilesSystem(GameContext game)
        {
            _game = game;
            _tilesFalling = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.ProcessedFalling,
                    GameMatcher.CanFall)
                .NoneOf(GameMatcher.FallDelay));
        }
    
        public void Execute()
        {
            for (var x = 0; x < 13; x++)
            {
                for (var y = 0; y <= 13; y++)
                {
                    var position = new Vector2Int(x, y);
                    var e = TileUtilsExtensions.GetTopTileByPosition(position);

                    if (_tilesFalling.ContainsEntity(e))
                    {
                        UpdateTransform(e);
                    }

                }
            }
        }

        private void UpdateTransform(GameEntity tileFalling)
        {
            var targetPosition = new Vector3(tileFalling.BoardPosition.x, tileFalling.BoardPosition.y);
            var currentPosition = tileFalling.worldPosition.Value;
            var distance = Vector3.Distance(currentPosition, targetPosition);

            if (distance >= 0.01f)
            {
                var step = tileFalling.FallDirection * 10f  * Time.deltaTime;
                    
                if (step.magnitude >= distance)
                {
                    tileFalling.isCanFall = false;

                    ResearchFallingProcess(tileFalling);
                }
                else
                {
                    tileFalling.ReplaceWorldPosition(currentPosition + step);
                }
            }
            else
            {
                tileFalling.isCanFall = false;
                
                ResearchFallingProcess(tileFalling);
            }
        }

        private void ResearchFallingProcess(GameEntity tileFalling)
        {
            if (MoveDown(tileFalling, tileFalling.BoardPosition))
            {
                var targetPosition1 = new Vector3(tileFalling.BoardPosition.x, tileFalling.BoardPosition.y);
                var currentPosition1 = tileFalling.worldPosition.Value;
                var distance1 = Vector3.Distance(currentPosition1, targetPosition1);
                
                if (distance1 >= 0.01f)
                {
                    var step = tileFalling.FallDirection * 10f * Time.deltaTime;
                
                    if (step.magnitude >= distance1)
                    {
                        tileFalling.ReplaceWorldPosition(targetPosition1);
                    }
                    else
                    {
                        tileFalling.ReplaceWorldPosition(currentPosition1 + step);
                    }
                }
            }
            else
            {
                tileFalling.ReplaceWorldPosition(new Vector3(tileFalling.BoardPosition.x, tileFalling.BoardPosition.y));
                // tileFalling.isProcessedFalling = false;
            }
        }

        private bool MoveDown(GameEntity e, Vector2Int position)
        {
            var nextRowPos = NextTilePositionForFalling.GetNextEmptyRow(Contexts.sharedInstance, position);
            
            var entityTopTile = TileUtilsExtensions.GetTopTileByPosition(nextRowPos);

            if (entityTopTile == null)
            {
                return false;
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
                // e.TileTweenAnimation.SetStartTransform();

                return true;
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

            return false;
        }
    }
}