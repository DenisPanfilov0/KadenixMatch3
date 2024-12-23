using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.SettingInGame.UI
{
    public class PrematureExitWindow : BaseWindow
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _closeWindow;
        
        private IWindowService _windowService;
        private IGameWinOrLoseUIService _gameWinOrLoseUIService;

        [Inject]
        public void Construct(IWindowService windowService, IGameWinOrLoseUIService gameWinOrLoseUIService)
        {
            _gameWinOrLoseUIService = gameWinOrLoseUIService;
            _windowService = windowService;
            Id = WindowId.PrematureExitWindow;

            SubscribeUpdates();
        }

        protected override void Initialize()
        {
            _exitButton.onClick.AddListener(Exit);
            _continueButton.onClick.AddListener(Continue);
            _closeWindow.onClick.AddListener(Continue);
        }

        protected override void UnsubscribeUpdates()
        {
            _exitButton.onClick.RemoveListener(Exit);
            _continueButton.onClick.RemoveListener(Continue);
            _closeWindow.onClick.RemoveListener(Continue);
        }

        private void Exit()
        {
            _gameWinOrLoseUIService.EnterMainMenu();
        }

        private void Continue()
        {
            _windowService.Close(Id);
        }
    }
}