using System.Collections.Generic;
using Code.Gameplay.Features.GoalsCounting.UI;
using Entitas;

namespace Code.Gameplay.Features.GoalsCounting.Systems
{
    public class GoalCheckSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tilesChecking;
        private readonly IGroup<GameEntity> _goals;
        private List<GameEntity> _buffer = new(64);
        private List<GameEntity> _bufferGoal = new(64);
        private IGoalsUIService _goalsUIService;

        public GoalCheckSystem(GameContext game, IGoalsUIService goalsUIService)
        {
            _goalsUIService = goalsUIService;
            
            _tilesChecking = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.GoalCheck)
                .NoneOf(GameMatcher.GoalMoving));
            
            _goals = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.GoalAmount,
                    GameMatcher.GoalType)
                .NoneOf(GameMatcher.GoalCompleted));
        }

        public void Execute()
        {
            foreach (GameEntity tileChecking in _tilesChecking.GetEntities(_buffer))
            {
                tileChecking.isAnimationProcess = true;

                foreach (GameEntity goal in _goals.GetEntities(_bufferGoal))
                {
                    if (tileChecking.TileType == goal.GoalType)
                    {
                        goal.ReplaceGoalAmount(goal.GoalAmount - 1);

                        tileChecking.isGoalMoving = true;
                        _goalsUIService.ChangeGoalCount(tileChecking, 1);
                        
                        tileChecking.isGoalCheck = false;

                        if (goal.GoalAmount == 0)
                        {
                            goal.isGoalCompleted = true;
                        }
                        
                        return;
                    }
                }

                _goalsUIService.ActiveAndDestroy(tileChecking);
                tileChecking.isGoalCheck = false;
            }
        }
    }
}