using System.Collections.Generic;
using Code.Gameplay.Features.CountingMoves.Services;
using Entitas;

namespace Code.Gameplay.Features.CountingMoves.Systems
{
    public class MovesIncreaseSystem : IExecuteSystem
    {
        private readonly IMovesInGameService _movesInGameService;
        private readonly IGroup<GameEntity> _moves;
        private List<GameEntity> _buffer = new(2);

        public MovesIncreaseSystem(GameContext game, IMovesInGameService movesInGameService)
        {
            _movesInGameService = movesInGameService;
            _moves = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Moves,
                    GameMatcher.IncreaseMoves));
        }
    
        public void Execute()
        {
            foreach (GameEntity move in _moves.GetEntities(_buffer))
            {
                move.ReplaceMoves(move.Moves + move.IncreaseMoves);
                
                _movesInGameService.SetMoves(move.Moves);

                move.RemoveIncreaseMoves();
            }
        }
    }
}