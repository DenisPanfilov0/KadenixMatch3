using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;

namespace Code.Gameplay.Features.GoalsCounting.Services
{       
    public class GameWinOrLoseUIService : IGameWinOrLoseUIService
    {
        private const string SceneName = "RestartLevel";
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _stateMachine;

        public GameWinOrLoseUIService(IWindowService windowService, IGameStateMachine stateMachine)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
        }

        public void OpenLoseWindow()
        {
            _windowService.Open(WindowId.GameLoseWindow);
        }

        public void OpenWinWindow()
        {
            _windowService.Open(WindowId.GameWinWindow);
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