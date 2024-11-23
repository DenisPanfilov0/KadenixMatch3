using Code.Gameplay.Features.FindMatchesFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.FindMatchesFeature
{
    public sealed class FindMatchesFeature : Feature
    {
        public FindMatchesFeature(ISystemFactory systems)
        {
            Add(systems.Create<IdenticalTilesSearchSystem>());
            Add(systems.Create<FigurePresenceCheckingSystem>());
            Add(systems.Create<TileGroupConvergeForBoosterSystem>());
            
            Add(systems.Create<CleanupFindMatchesSystem>());
            Add(systems.Create<CleanupSelectTilesForMatchesSystem>());
        }
    }
}