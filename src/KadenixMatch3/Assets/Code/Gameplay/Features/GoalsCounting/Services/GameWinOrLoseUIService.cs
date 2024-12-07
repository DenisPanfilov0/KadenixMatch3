using System.Collections.Generic;
using Code.Gameplay.Features.CountingMoves.Services;
using Code.Gameplay.Features.GoalsCounting.Configs;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.Feature.Gold.Services;
using Code.Meta.Feature.Heart.Services;
using Code.Meta.Feature.StreakLevelsRewarded.Services;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.GoalsCounting.Services
{       
    public class GameWinOrLoseUIService : IGameWinOrLoseUIService
    {
        public CostToContinuePlayingConfig CostToContinuePlayingConfig => _costToContinuePlayingConfig;
        
        private const string SceneName = "RestartLevel";
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progress;
        private readonly IStreakLevelsRewardUIService _streakLevelsRewardUIService;
        private readonly ICharacterGoldUIService _characterGoldUIService;
        private readonly ICharacterHeartUIService _characterHeartUIService;
        private readonly IMovesInGameService _movesInGameService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _boards;

        private readonly CostToContinuePlayingConfig _costToContinuePlayingConfig;

        public GameWinOrLoseUIService(IWindowService windowService, IGameStateMachine stateMachine, 
            IProgressProvider progress, IStreakLevelsRewardUIService streakLevelsRewardUIService,
            ICharacterGoldUIService characterGoldUIService, ICharacterHeartUIService characterHeartUIService,
            GameContext game, IMovesInGameService movesInGameService, IStaticDataService staticDataService)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
            _progress = progress;
            _streakLevelsRewardUIService = streakLevelsRewardUIService;
            _characterGoldUIService = characterGoldUIService;
            _characterHeartUIService = characterHeartUIService;
            _movesInGameService = movesInGameService;
            _staticDataService = staticDataService;

            _boards = game.GetGroup(GameMatcher.BoardState);

            _costToContinuePlayingConfig = _staticDataService.GetCostToContinuePlayingConfig();
        }

        public void OpenLoseWindow()
        {
            if (_costToContinuePlayingConfig.CurrentNumberIteration < 3)
            {
                _windowService.Open(WindowId.PreLoseWindow);
                _costToContinuePlayingConfig.CurrentNumberIteration++;
            }
            else
            {
                GameLose();
            }
        }

        public void GameLose()
        {
            _windowService.Open(WindowId.GameLoseWindow);
            Cleanup();
        }

        public void OpenWinWindow()
        {
            _windowService.Open(WindowId.GameWinWindow);
            _characterGoldUIService.IncreaseGold(300); _characterHeartUIService.IncreaseHeart(1);
            Cleanup();
        }
        
        public void Continue()
        {
            _streakLevelsRewardUIService.AddNumbersWins();
            _progress.ProgressData.ProgressModel.CurrentLevel++;
            _stateMachine.Enter<LoadingHomeScreenState>();
            Cleanup();
        }

        public void RestartLevel()
        {
            _stateMachine.Enter<RestartMatch3LevelState, string>(SceneName);
            Cleanup();
        }

        public void EnterMainMenu()
        {
            _stateMachine.Enter<LoadingHomeScreenState>();
            Cleanup();
        }

        public void AddMovesForVideo()
        {
            Debug.Log("Посмтрели рекламу" + UnityEngine.Time.time);
            AddMoves();
        }

        public void AddMovesForCoins()
        {
            _characterGoldUIService
                .DecreaseGold(_costToContinuePlayingConfig.CostToContinuePlayingsConfigs[_costToContinuePlayingConfig.CurrentNumberIteration - 1].Cost);
            AddMoves();
        }

        public bool IsItAvailablePurchase()
        {
            return _progress.ProgressData.ProgressModel.Coins >=
                _costToContinuePlayingConfig
                    .CostToContinuePlayingsConfigs[_costToContinuePlayingConfig.CurrentNumberIteration].Cost;
        }

        private void AddMoves()
        {
            foreach (GameEntity board in _boards)
            {
                board.isStopGame = false;
            }
            
            _movesInGameService.IncreaseMoves(1);
        }

        private void Cleanup()
        {
            _costToContinuePlayingConfig.CurrentNumberIteration = 0;
        }
    }
}