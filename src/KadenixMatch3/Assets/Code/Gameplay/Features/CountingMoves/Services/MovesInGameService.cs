using System;
using Entitas;

namespace Code.Gameplay.Features.CountingMoves.Services
{
    public class MovesInGameService : IMovesInGameService
    {
        public event Action MovesChange;
        
        private readonly IGroup<GameEntity> _moves;
        private int _movesAmount;

        public MovesInGameService()
        {
            _moves = Contexts.sharedInstance.game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Moves));
        }

        public void SetMoves(int count)
        {
            _movesAmount = count;
            MovesChange?.Invoke();
        }

        public void DecreaseMoves(int count)
        {
            _movesAmount -= count;
            MovesChange?.Invoke();
        }

        public void IncreaseMoves(int count)
        {
            foreach (var move in _moves)
            {
                move.AddIncreaseMoves(count);
                _movesAmount += count;
            }
            
            MovesChange?.Invoke();
        }

        public int GetMoves() => 
            _movesAmount;
    }
}