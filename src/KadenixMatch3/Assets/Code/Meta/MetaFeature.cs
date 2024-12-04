using Code.Common.Destruct;
using Code.Infrastructure.Systems;
using Code.Meta.Feature.Gold;

namespace Code.Meta
{
    public sealed class MetaFeature : global::Feature
    {
        public MetaFeature(ISystemFactory systems)
        {
            Add(systems.Create<GoldFeature>());
            
            Add(systems.Create<ProcessDestructedFeature>());
        }
    }
}