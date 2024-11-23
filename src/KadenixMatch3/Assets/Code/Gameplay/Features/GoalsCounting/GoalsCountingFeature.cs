using Code.Gameplay.Features.GoalsCounting.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.GoalsCounting
{
    public sealed class GoalsCountingFeature : Feature
    {
        public GoalsCountingFeature(ISystemFactory systems)
        {
            Add(systems.Create<GoalCreateSystem>());
                
            Add(systems.Create<GoalCheckSystem>());
            
            Add(systems.Create<WinLoseCheckSystem>());
        }
    }
}