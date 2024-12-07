using Code.Gameplay.Features.BoardState.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.BoardState
{
    public sealed class BoardStateFeature : Feature
    {
        public BoardStateFeature(ISystemFactory systems)
        {
            Add(systems.Create<BoardStateSetupSystem>());
            
            Add(systems.Create<CheckBoardActiveSystem>());
        }
    }
}