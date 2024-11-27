using System;
using Code.Progress.Provider;
using UnityEngine;
using Zenject;

namespace Code.TestTools
{
    public class CurrentProgressModel : MonoBehaviour
    {
        public int CurrentLevel;
        public int CurrentStreak;
        
        private IProgressProvider _progress;

        [Inject]
        public void Construct(IProgressProvider progress)
        {
            _progress = progress;
        }

        private void Start()
        {
            CurrentLevel = _progress.ProgressData.ProgressModel.CurrentLevel;
            CurrentStreak = _progress.ProgressData.ProgressModel.SequentialLevelsReward.CurrentStreak;
        }
    }
}