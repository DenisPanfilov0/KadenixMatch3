using Code.Gameplay.Features.ActiveStartStateTilesFeature.Systems;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.ActiveStartStateTilesFeature
{
    public sealed class ActiveStartStateTilesFeature : Feature
    {
        public ActiveStartStateTilesFeature(ISystemFactory systems)
        {
            Add(systems.Create<GrassModifierActiveStartStateSystem>());
            Add(systems.Create<IceModifierActiveStartStateSystem>());
        }
    }
}