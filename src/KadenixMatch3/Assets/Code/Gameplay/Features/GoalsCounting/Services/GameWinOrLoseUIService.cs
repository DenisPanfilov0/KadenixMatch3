using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.Feature.StreakLevelsRewarded.Services;
using Code.Progress.Provider;

namespace Code.Gameplay.Features.GoalsCounting.Services
{       
    public class GameWinOrLoseUIService : IGameWinOrLoseUIService
    {
        private const string SceneName = "RestartLevel";
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progress;
        private readonly IStreakLevelsRewardUIService _streakLevelsRewardUIService;

        public GameWinOrLoseUIService(IWindowService windowService, IGameStateMachine stateMachine, 
            IProgressProvider progress, IStreakLevelsRewardUIService streakLevelsRewardUIService)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
            _progress = progress;
            _streakLevelsRewardUIService = streakLevelsRewardUIService;
        }

        public void OpenLoseWindow()
        {
            _windowService.Open(WindowId.GameLoseWindow);
        }

        public void OpenWinWindow()
        {
            _windowService.Open(WindowId.GameWinWindow);
        }
        
        public void Continue()
        {
            _streakLevelsRewardUIService.AddNumbersWins();
            _progress.ProgressData.ProgressModel.CurrentLevel++;
            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        public void RestartLevel()
        {
            _stateMachine.Enter<RestartMatch3LevelState, string>(SceneName);
        }

        public void EnterMainMenu()
        {
            _stateMachine.Enter<LoadingHomeScreenState>();
        }
    }
}