using System;
using Code.Gameplay.Features.BoardBuildFeature;

namespace Code.Gameplay.Features.GoalsCounting.UI
{
    public class GoalsUIService : IGoalsUIService
    {
        public event Action<TileTypeId, int> OnChangeGoal;
        
        public GoalsUIService()
        {
        }

        public void ChangeGoalCount(GameEntity entity, int amount)
        {
            // if (в зависимости от типа, вызывает на GoalAnimationRegistrar определенную анимацию и ожидаем колбэка)
            // {
            //     
            // }

            switch (entity.TileType)
            {
                case TileTypeId.coloredRed:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.coloredYellow:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.coloredBlue:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.coloredGreen:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.coloredPurple:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.tileModifierSpawner:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.powerUpVerticalRocket:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.powerUpHorizontalRocket:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.powerUpBomb:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.powerUpMagicBall:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.grassModifier:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
                    break;
                case TileTypeId.iceModifier:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity, () => { InvokeGoalChange(entity.TileType, 1);});
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
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.powerUpHorizontalRocket:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.powerUpBomb:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.powerUpMagicBall:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.grassModifier:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
                case TileTypeId.iceModifier:
                    entity.ColoredTileAnimation.TilesOnDestroy(entity);
                    break;
            }
        }

        private void InvokeGoalChange(TileTypeId tileType, int amount)
        {
            OnChangeGoal?.Invoke(tileType, amount);
        }
    }
}