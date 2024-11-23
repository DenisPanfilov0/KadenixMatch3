using Code.Gameplay.Features.PowerUpGeneratedFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.PowerUpGeneratedFeature
{
    public sealed class PowerUpGeneratedFeature : Feature
    {
        public PowerUpGeneratedFeature(ISystemFactory systems)
        {
            Add(systems.Create<PowerUpTileMarkSystem>());
            
            Add(systems.Create<PowerUpGeneratedSystem>());
            Add(systems.Create<PowerUpGeneratedByTypeSystem>());
            
            Add(systems.Create<PowerUpSpawnAnimationSystem>());
        }
    }
}