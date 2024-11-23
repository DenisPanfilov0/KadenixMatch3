using System.Collections.Generic;
using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Windows;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.GoalsCounting.Systems
{
    public class WinLoseCheckSystem : IExecuteSystem
    {
        private readonly IGameWinOrLoseUIService _gameWinOrLoseUIService;
        private readonly IProgressProvider _progress;
        private readonly IGroup<GameEntity> _goals;
        private readonly Level _lvl;
        private List<GameEntity> _buffer = new(4);

        public WinLoseCheckSystem(GameContext game, IGameWinOrLoseUIService gameWinOrLoseUIService, IProgressProvider progress)
        {
            _gameWinOrLoseUIService = gameWinOrLoseUIService;
            _progress = progress;

            _goals = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.GoalAmount,
                    GameMatcher.GoalType,
                    GameMatcher.GoalCompleted)
                .NoneOf(GameMatcher.GameWin));
            
            _lvl = _progress.ProgressData.ProgressModel.Levels[_progress.ProgressData.ProgressModel.CurrentLevel - 1];
        }

        public void Execute()
        {
            if (_goals.count == _lvl.goals.Count)
            {
                _gameWinOrLoseUIService.OpenWinWindow();

                foreach (GameEntity goal in _goals.GetEntities(_buffer))
                {
                    goal.isGameWin = true;
                }
            }
        }
    }
}