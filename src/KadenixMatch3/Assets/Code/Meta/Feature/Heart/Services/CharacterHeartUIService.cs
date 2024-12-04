using System;
using Code.Progress.Provider;

namespace Code.Meta.Feature.Heart.Services
{
    public class CharacterHeartUIService : ICharacterHeartUIService
    {
        public event Action HeartChange;
        
        private readonly IProgressProvider _progress;

        public CharacterHeartUIService(IProgressProvider progress)
        {
            _progress = progress;
        }

        public int GetCountHearts()
        {
            return _progress.ProgressData.ProgressModel.Heart;
        }
        
        public void IncreaseHeart(int count)
        {
            _progress.ProgressData.ProgressModel.Heart += count;
            HeartChange?.Invoke();
        }
        
        public void DecreaseHeart(int count)
        {
            _progress.ProgressData.ProgressModel.Heart -= count;
            HeartChange?.Invoke();
        }
        
    }
}