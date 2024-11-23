using Code.Gameplay.Features.Input.Systems;
using Code.Gameplay.Features.TilesFallFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.TilesFallFeature
{
    public sealed class TilesFallFeature : Feature
    {
        public TilesFallFeature(ISystemFactory systems)
        {
            // Add(systems.Create<TileFallingProcessedSystem>());
            Add(systems.Create<DirectionalDeltaMoveTilesSystem>());
            
            Add(systems.Create<UpdateTransformPositionSystem>());

            // Add(systems.Create<FallAccelerationSystem>());
            Add(systems.Create<DelayReductionSystem>());

            Add(systems.Create<CheckingCanFallTileSystem>());
            Add(systems.Create<TileEndFallingSystem>());
            
        }
    }
}