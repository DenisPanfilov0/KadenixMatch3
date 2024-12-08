using System;
using Code.Gameplay.Features.GoalsCounting.Services;
using UnityEngine;
using Zenject;

namespace Code.TestTools
{
    public class RestartLevelGeneration : MonoBehaviour
    {
        private IGameWinOrLoseUIService _gameWinOrLoseUIService;
        private bool _actionTriggered; // Флаг для отслеживания выполнения действия

        [Inject]
        public void Construct(IGameWinOrLoseUIService gameWinOrLoseUIService)
        {
            _gameWinOrLoseUIService = gameWinOrLoseUIService;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12) && !_actionTriggered)
            {
                _actionTriggered = true; // Устанавливаем флаг
                _gameWinOrLoseUIService.RestartLevel();
            }
        }
    }
}