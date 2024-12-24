using System;
using Code.Meta.Feature.Heart.Services;
using Code.Progress.Provider;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.Feature.Heart.UI
{
    public class LifeRegenerationTimer : MonoBehaviour
    {
        private IProgressProvider _progressProvider;
        private float _updateInterval = 1f;
        private int _lifeRegenerationInterval = 900;

        [SerializeField] private TextMeshProUGUI _timerText;

        private ICharacterHeartUIService _characterHeartUIService;

        [Inject]
        public void Initialize(IProgressProvider progressProvider, ICharacterHeartUIService characterHeartUIService)
        {
            _characterHeartUIService = characterHeartUIService;
            _progressProvider = progressProvider;
        }

        private void Start()
        {
            UpdateUI();
        }

        private void Update()
        {
            if (_progressProvider.ProgressData.ProgressModel.Heart < 5)
            {
                LifeRegeneration();
                UpdateTimerUI();
            }
            else
            {
                _timerText.text = "MAX";
            }
        }

        private void LifeRegeneration()
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long timeSinceLastRegeneration = currentTimestamp - _progressProvider.ProgressData.ProgressModel.LastLifeRegenerationTime;

            if (_progressProvider.ProgressData.ProgressModel.LastLifeRegenerationTime == 0)
            {
                SaveProgressWithTimestamp(currentTimestamp);
                return;
            }

            int livesToAdd = (int)(timeSinceLastRegeneration / _lifeRegenerationInterval);

            if (livesToAdd > 0)
            {
                _characterHeartUIService.IncreaseHeart(1);

                _progressProvider.ProgressData.ProgressModel.LastLifeRegenerationTime += livesToAdd * _lifeRegenerationInterval;

                if (_progressProvider.ProgressData.ProgressModel.Heart >= 5)
                {
                    _progressProvider.ProgressData.ProgressModel.Heart = 5;
                    _progressProvider.ProgressData.ProgressModel.LastLifeRegenerationTime = 0;
                }
            }
        }

        private void SaveProgressWithTimestamp(long currentTimestamp)
        {
            _progressProvider.ProgressData.ProgressModel.LastLifeRegenerationTime = currentTimestamp;
        }

        private void UpdateTimerUI()
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long timeSinceLastRegeneration = currentTimestamp - _progressProvider.ProgressData.ProgressModel.LastLifeRegenerationTime;

            int timeLeft = _lifeRegenerationInterval - (int)(timeSinceLastRegeneration % _lifeRegenerationInterval);

            if (timeLeft > 0)
            {
                int minutes = timeLeft / 60;
                int seconds = timeLeft % 60;
                _timerText.text = $"{minutes:D2}:{seconds:D2}";
            }
            else
            {
                _timerText.text = "00:00";
            }
        }

        private void UpdateUI()
        {
            if (_progressProvider.ProgressData.ProgressModel.Heart >= 5)
            {
                _timerText.text = "MAX";
            }
            else
            {
                UpdateTimerUI();
            }
        }
    }
}
