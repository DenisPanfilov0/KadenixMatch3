using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.GoalsCounting.UI;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;

namespace Code.Gameplay.Features.GoalsCounting.Systems
{
    public class GoalCreateSystem : IInitializeSystem
    {
        private IProgressProvider _progress;
        private readonly IGoalsUIService _goalsUIService;
        private readonly Level _lvl;

        public GoalCreateSystem(GameContext game, IProgressProvider progress, IGoalsUIService goalsUIService)
        {
            _progress = progress;
            _goalsUIService = goalsUIService;

            _lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);
        }
    
        public void Initialize()
        {
            foreach (var goal in _lvl.goals)
            {
                CreateEntity.Empty()
                    .AddGoalType(TileTypeParserExtensions.TileTypeResolve(goal.Key))
                    .AddGoalAmount(goal.Value);
                
                _goalsUIService.CreateGoal(goal);
            }
        }
    }
}