using System;
using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.GoalsCounting.UI;
using UnityEngine;

namespace Code.Gameplay.Features.GoalsCounting.Services
{
    public class GoalsUIService : IGoalsUIService
    {
        public event Action<TileTypeId, int> OnChangeGoal;
        public event Action<KeyValuePair<string, int>> OnCreateGoal;

        private List<GoalItem> _goals = new();
        
        public GoalsUIService()
        {
        }

        public void CreateGoal(KeyValuePair<string, int> goal)
        {
            OnCreateGoal?.Invoke(goal);
        }

        public void AddGoalInPool(GoalItem goal)
        {
            _goals.Add(goal);
        }

        public Transform GetGoalTransform(TileTypeId goalType)
        {
            foreach (var goal in _goals)
            {
                if (goal.GoalType == goalType)
                {
                    return goal.gameObject.transform;
                }
            }

            return null;
        }

        public void TileSwap(GameEntity entity, Vector3 direction)
        {
            entity.BaseTileAnimation.TileSwap(entity, direction);
        }

        public void ChangeGoalCount(GameEntity entity, int amount)
        {
            entity.BaseTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
        }
        
        public void ActiveAndDestroy(GameEntity entity)
        {
            entity.BaseTileAnimation.TilesOnDestroy(entity);
        }

        public void Cleanup()
        {
            _goals.Clear();
        }

        private void InvokeGoalChange(GameEntity entity, int amount)
        {
            OnChangeGoal?.Invoke(entity.TileType, amount);
            entity.isGoalMoving = false;
        }
    }
}