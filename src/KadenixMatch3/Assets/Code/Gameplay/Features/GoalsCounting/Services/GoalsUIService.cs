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

        public void ChangeGoalCount(GameEntity entity, int amount)
        {
            switch (entity.TileType)
            {
                case TileTypeId.coloredRed:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.coloredYellow:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.coloredBlue:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.coloredGreen:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.coloredPurple:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.tileModifierSpawner:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.powerUpVerticalRocket:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.powerUpHorizontalRocket:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.powerUpBomb:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.grassModifier:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
                case TileTypeId.iceModifier:
                    entity.ColoredTileAnimation.MoveTileToTarget(entity, () => { InvokeGoalChange(entity, 1);});
                    break;
            }
            
            // OnChangeGoal?.Invoke(entity.TileType, amount);
        }
        
        public void ActiveAndDestroy(GameEntity entity)
        {
            switch (entity.TileType)
            {
                case TileTypeId.coloredRed:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.coloredYellow:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.coloredBlue:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.coloredGreen:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.coloredPurple:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.tileModifierSpawner:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.powerUpVerticalRocket:
                    entity.PowerUpVerticalRocketAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.powerUpHorizontalRocket:
                    entity.PowerUpHorizontalRocketAnimation.TilesOnDestroy(entity);
                    break;
                
                
                // case TileTypeId.powerUpBomb:
                //     entity.PowerUpBombAnimation.TilesOnDestroy(entity);
                //     break;
                case TileTypeId.powerUpBomb:
                    entity.BaseTileAnimation.TilesOnDestroy(entity);
                    break;
                
                
                case TileTypeId.powerUpMagicBall:
                    entity.MagicBallTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.grassModifier:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.iceModifier:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
            }
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