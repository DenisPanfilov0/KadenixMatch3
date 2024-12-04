using System.Collections.Generic;
using Code.Gameplay.Features.CountingMoves.Services;
using Entitas;

namespace Code.Gameplay.Features.CountingMoves.Systems
{
    public class MovesDecreaseSystem : IExecuteSystem
    {
        private readonly IMovesInGameService _movesInGameService;
        private readonly IGroup<GameEntity> _moves;
        private List<GameEntity> _buffer = new(2);

        public MovesDecreaseSystem(GameContext game, IMovesInGameService movesInGameService)
        {
            _movesInGameService = movesInGameService;
            
            _moves = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Moves,
                    GameMatcher.DecreaseMoves)
                .NoneOf(GameMatcher.MovesChangeAmountProcess));
        }
    
        public void Execute()
        {
            foreach (GameEntity move in _moves.GetEntities(_buffer))
            {
                move.ReplaceMoves(move.Moves - move.DecreaseMoves);
                
                // _movesInGameService.DecreaseMoves(move.DecreaseMoves);
                _movesInGameService.SetMoves(move.Moves);

                move.RemoveDecreaseMoves();
            }
        }
    }
}