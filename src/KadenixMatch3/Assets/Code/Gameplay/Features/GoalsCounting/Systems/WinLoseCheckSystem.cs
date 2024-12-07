using System.Collections.Generic;
using System.Linq;
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
        private readonly IGroup<GameEntity> _boards;
        private readonly IGroup<GameEntity> _moves;

        public WinLoseCheckSystem(GameContext game, IGameWinOrLoseUIService gameWinOrLoseUIService, IProgressProvider progress)
        {
            _gameWinOrLoseUIService = gameWinOrLoseUIService;
            _progress = progress;

            _goals = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.GoalAmount,
                    GameMatcher.GoalType,
                    GameMatcher.GoalCompleted)
                .NoneOf(GameMatcher.GameWin, GameMatcher.GameLose));

            _moves = game.GetGroup(GameMatcher.Moves);

            _boards = game.GetGroup(GameMatcher.BoardState);
            
            _lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);
        }

        public void Execute()
        {
            foreach (GameEntity move in _moves)
            foreach (GameEntity board in _boards)
            {
                if (board.isBoardActiveInteraction || board.isStopGame)
                {
                    return;
                }

                if (_goals.count == _lvl.goals.Count)
                {
                    _gameWinOrLoseUIService.OpenWinWindow();

                    board.isStopGame = true;

                    // foreach (GameEntity goal in _goals.GetEntities(_buffer))
                    // {
                    //     goal.isGameWin = true;
                    // }
                }
                else if (move.Moves <= 0)
                {
                    _gameWinOrLoseUIService.OpenLoseWindow();

                    board.isStopGame = true;

                    // foreach (GameEntity goal in _goals.GetEntities(_buffer))
                    // {
                    //     goal.isGameLose = true;
                    // }
                } 
            }
        }
    }
}