using Code.Progress.Provider;

namespace Code.Meta.Feature.StreakLevelsRewarded.Services
{
    public class StreakLevelsRewardUIService : IStreakLevelsRewardUIService
    {
        private readonly IProgressProvider _progress;

        public StreakLevelsRewardUIService(IProgressProvider progress)
        {
            _progress = progress;
        }
        
        public void AddNumbersWins()
        {
            _progress.ProgressData.ProgressModel.SequentialLevelsReward.CurrentStreak++;
        }

        public void LevelRewardCompleted()
        {
            _progress.ProgressData.ProgressModel.SequentialLevelsReward.CurrentStreak = 0;
            _progress.ProgressData.ProgressModel.SequentialLevelsReward.LevelReward++;
        }

        public int GetCurrentStreak() => _progress.ProgressData.ProgressModel.SequentialLevelsReward.CurrentStreak;
        
        public int GetMaxStreak() => _progress.ProgressData.ProgressModel.SequentialLevelsReward.TargetStreak;
        
        public int GetCurrentLevelReward() => _progress.ProgressData.ProgressModel.SequentialLevelsReward.LevelReward;
        
        public int GetMaxLevelReward() => _progress.ProgressData.ProgressModel.SequentialLevelsReward.MaxLevelReward;
    }
}