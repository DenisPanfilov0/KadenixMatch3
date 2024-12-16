using System;
using Code.Progress.Provider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.TestTools
{
    public class CurrentProgressModel : MonoBehaviour
    {
        public int CurrentStreak;

        [SerializeField] private Button _changeLevel;
        [SerializeField] private TMP_InputField _newLevelValue;
        [SerializeField] private TextMeshProUGUI _currentLevel;

        private IProgressProvider _progress;

        [Inject]
        public void Construct(IProgressProvider progress)
        {
            _progress = progress;
        }

        private void Start()
        {
            _currentLevel.text = _progress.ProgressData.ProgressModel.CurrentLevel.ToString();
            CurrentStreak = _progress.ProgressData.ProgressModel.SequentialLevelsReward.CurrentStreak;
            
            _changeLevel.onClick.AddListener(ChangeCurrentLevel);
        }

        private void ChangeCurrentLevel()
        {
            if (int.TryParse(_newLevelValue.text, out int newLevel))
            {
                _progress.ProgressData.ProgressModel.CurrentLevel = newLevel;
            }
            
            _currentLevel.text = $"Current Level: {_progress.ProgressData.ProgressModel.CurrentLevel.ToString()}";
        }
    }
}