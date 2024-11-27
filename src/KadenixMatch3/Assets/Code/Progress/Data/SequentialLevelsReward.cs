using System;

namespace Code.Progress.Data
{
    [Serializable]
    public class SequentialLevelsReward
    {
        public int CurrentStreak;
        public int TargetStreak;
        public int LevelReward;
        public int MaxLevelReward;
        public bool IsRewardClaimed;
        public bool IsRewardCompleted;

        public SequentialLevelsReward()
        {
            CurrentStreak = 0;
            TargetStreak = 3;
            LevelReward = 1;
            MaxLevelReward = 3;
            IsRewardClaimed = false;
            IsRewardCompleted = false;
        }
    }
}