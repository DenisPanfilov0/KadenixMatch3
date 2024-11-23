using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
    public class InputSwapSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _firstSelectTiles;
        private readonly IGroup<GameEntity> _secondSelectTiles;
        private List<GameEntity> _buffer = new(4);

        public InputSwapSystem(GameContext game)
        {
            _firstSelectTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.FirstSelectTileSwipe).NoneOf(GameMatcher.TileSwipeProcessed));
            _secondSelectTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.SecondSelectTileSwipe).NoneOf(GameMatcher.TileSwipeProcessed));
        }
    
        public void Execute()
        {
            foreach (var firstSelectTile in _firstSelectTiles.GetEntities(_buffer))
            foreach (var secondSelectTile in _secondSelectTiles.GetEntities(_buffer))
            {
                var firstTilePosition = firstSelectTile.BoardPosition;
                var secondTilePosition = secondSelectTile.BoardPosition;

                firstSelectTile.ReplaceBoardPosition(secondTilePosition);
                secondSelectTile.ReplaceBoardPosition(firstTilePosition);

                firstSelectTile.isTileSwipeProcessed = true;
                secondSelectTile.isTileSwipeProcessed = true;
                
                secondSelectTile.TileTweenAnimation.SetStartTransform();
                firstSelectTile.TileTweenAnimation.SetStartTransform();

                int firstTileQueue = firstSelectTile.PositionInCoverageQueue;
                int secondTileQueue = secondSelectTile.PositionInCoverageQueue;
                
                secondSelectTile.ReplacePositionInCoverageQueue(firstTileQueue);
                firstSelectTile.ReplacePositionInCoverageQueue(secondTileQueue); 

                firstSelectTile.ReplaceSwipeDirection(
                    new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y)
                    - new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y));
                
                secondSelectTile.ReplaceSwipeDirection(
                    new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y)
                    - new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y));
                
                // firstSelectTile.Transform.position = new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y);
                // secondSelectTile.Transform.position = new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y);
                
                return;
            }
        }
    }
}