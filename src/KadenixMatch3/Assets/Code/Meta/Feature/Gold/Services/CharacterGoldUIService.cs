using System;
using Code.Progress.Provider;

namespace Code.Meta.Feature.Gold.Services
{
    public class CharacterGoldUIService : ICharacterGoldUIService
    {
        public event Action GoldChange;
        
        private readonly IProgressProvider _progress;

        public CharacterGoldUIService(IProgressProvider progress)
        {
            _progress = progress;
        }

        public int GetCountCoins()
        {
            return _progress.ProgressData.ProgressModel.Coins;
        }

        public void IncreaseGold(int count)
        {
            _progress.ProgressData.ProgressModel.Coins += count;
            GoldChange?.Invoke();
        }
        
        public void DecreaseGold(int count)
        {
            _progress.ProgressData.ProgressModel.Coins -= count;
            GoldChange?.Invoke();
        }
    }
}