using Code.Gameplay.Features.TileGenerationFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.TileGenerationFeature
{
    public sealed class TileGenerationFeature : Feature
    {
        public TileGenerationFeature(ISystemFactory systems)
        {
            Add(systems.Create<TileGenerationInSpawnersSystem>());
        }
    }
}