using Code.Infrastructure.Systems;
using Code.Meta.Feature.Gold.Systems;

namespace Code.Meta.Feature.Gold
{
    public sealed class GoldFeature : global::Feature
    {
        public GoldFeature(ISystemFactory systems)
        {
            Add(systems.Create<IncreaseGoldSystem>());
        }
    }
}