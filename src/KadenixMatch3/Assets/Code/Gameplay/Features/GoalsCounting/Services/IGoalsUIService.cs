using System;
using Code.Gameplay.Features.BoardBuildFeature;

namespace Code.Gameplay.Features.GoalsCounting.UI
{
    public interface IGoalsUIService
    {
        event Action<TileTypeId, int> OnChangeGoal;
        void ChangeGoalCount(GameEntity entity, int amount);
        void ActiveAndDestroy(GameEntity entity);
    }
}