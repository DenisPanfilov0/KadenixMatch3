using System;
using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature;
using UnityEngine;

namespace Code.Gameplay.Features.GoalsCounting.UI
{
    public interface IGoalsUIService
    {
        event Action<TileTypeId, int> OnChangeGoal;
        void ChangeGoalCount(GameEntity entity, int amount);
        void ActiveAndDestroy(GameEntity entity);
        void CreateGoal(KeyValuePair<string, int> goal);
        event Action<KeyValuePair<string, int>> OnCreateGoal;
        void AddGoalInPool(GoalItem goal);
        Transform GetGoalTransform(TileTypeId goalType);
        void Cleanup();
        void TileSwap(GameEntity entity, Vector3 direction);
    }
}