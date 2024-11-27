using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.Common.Extension;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;

namespace Code.Gameplay.Features.GoalsCounting.Systems
{
    public class GoalCreateSystem : IInitializeSystem
    {
        private IProgressProvider _progress;
        private readonly Level _lvl;

        public GoalCreateSystem(GameContext game, IProgressProvider progress)
        {
            _progress = progress;
            
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
            }
        }
    }
}