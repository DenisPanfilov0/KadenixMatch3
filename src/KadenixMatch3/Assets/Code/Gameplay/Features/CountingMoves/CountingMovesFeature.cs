using Code.Gameplay.Features.CountingMoves.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.CountingMoves
{
    public sealed class CountingMovesFeature : Feature
    {
        public CountingMovesFeature(ISystemFactory systems)
        {
            Add(systems.Create<MovesSetupSystem>());
            
            Add(systems.Create<MovesIncreaseSystem>());
            Add(systems.Create<MovesDecreaseSystem>());
        }
    }
}