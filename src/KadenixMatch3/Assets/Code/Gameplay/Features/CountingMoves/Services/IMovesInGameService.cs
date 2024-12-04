using System;

namespace Code.Gameplay.Features.CountingMoves.Services
{
    public interface IMovesInGameService
    {
        event Action MovesChange;
        void SetMoves(int count);
        void DecreaseMoves(int count);
        void IncreaseMoves(int count);
        int GetMoves();
    }
}