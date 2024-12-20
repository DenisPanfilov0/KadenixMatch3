using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
    public class InputDoubleClickSystem : IExecuteSystem
    {
        private List<GameEntity> _buffer = new(4);
        private List<GameEntity> _bufferMoves = new(4);
        private readonly IGroup<GameEntity> _clickTiles;
        private readonly IGroup<GameEntity> _moves;

        public InputDoubleClickSystem(GameContext game)
        {
            _clickTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.TileForDoubleClick));
            _moves = game.GetGroup(GameMatcher.AllOf(GameMatcher.Moves).NoneOf(GameMatcher.MovesChangeAmountProcess));
        }
    
        public void Execute()
        {
            foreach (var clickTile in _clickTiles.GetEntities(_buffer))
            {
                clickTile.isGoalCheck = true;
                clickTile.isActiveInteraction = true;
                clickTile.isTileForDoubleClick = false;
                
                foreach (GameEntity move in _moves.GetEntities(_bufferMoves))
                {
                    move.AddDecreaseMoves(1);
                    // move.isMovesChangeAmountProcess = true;
                }
                
                return;
            }
        }
    }
}