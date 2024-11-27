namespace Code.Meta.Feature.StreakLevelsRewarded.Services
{
    public interface IStreakLevelsRewardUIService
    {
        void AddNumbersWins();
        int GetCurrentStreak();
        int GetMaxStreak();
        int GetCurrentLevelReward();
        int GetMaxLevelReward();
        void LevelRewardCompleted();
    }
}