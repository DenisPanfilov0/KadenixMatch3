using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.ActiveInteractionFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.ActiveInteractionFeature
{
    public sealed class ActiveInteractionFeature : Feature
    {
        public ActiveInteractionFeature(ISystemFactory systems)
        {
            Add(systems.Create<MagicBallAndCrystalInteractionSystem>());
            Add(systems.Create<BombAndBombInteractionSystem>());
            Add(systems.Create<BombAndRocketInteractionSystem>());
            Add(systems.Create<RocketAndRocketInteractionSystem>());
            
            Add(systems.Create<ColoredCrystalInteractionSystem>());
            Add(systems.Create<HorizontalRocketInteractionSystem>());
            Add(systems.Create<VerticalRocketInteractionSystem>());
            Add(systems.Create<BombInteractionSystem>());
            
            Add(systems.Create<GrassModifierInteractionSystem>());
            
            Add(systems.Create<IceModifierInteractionSystem>());
            
            Add(systems.Create<SearchDelayInteractionSystem>());
        }
    }
}