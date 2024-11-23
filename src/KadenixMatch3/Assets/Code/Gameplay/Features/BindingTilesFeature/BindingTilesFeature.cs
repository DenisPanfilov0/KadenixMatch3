using Code.Gameplay.Features.BindingTilesFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.BindingTilesFeature
{
    public sealed class BindingTilesFeature : Feature
    {
        public BindingTilesFeature(ISystemFactory systems)
        {
            Add(systems.Create<BindingTilesSystem>());
        }
    }
}