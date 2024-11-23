using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.GoalsCounting.UI.WinLoseWindows
{
    public class GameWinWindow : BaseWindow
    {
        [SerializeField] private Button _openMainMenu;
        [SerializeField] private Button _restartLevel;
        private IGameWinOrLoseUIService _gameWinOrLoseUIService;
        private IWindowService _windowService;

        [Inject]
        private void Construct(IGameWinOrLoseUIService gameWinOrLoseUIService, IWindowService windowService)
        {
            _windowService = windowService;
            _gameWinOrLoseUIService = gameWinOrLoseUIService;
            Id = WindowId.ShopWindow;
        }

        protected override void Initialize()
        {
            _openMainMenu.onClick.AddListener(EnterMainMenu);
            _restartLevel.onClick.AddListener(RestartLevel);
        }

        protected override void UnsubscribeUpdates()
        {
            _openMainMenu.onClick.RemoveListener(EnterMainMenu);
            _restartLevel.onClick.RemoveListener(RestartLevel);
        }

        private void EnterMainMenu()
        {
            _gameWinOrLoseUIService.EnterMainMenu();
            _windowService.Close(Id);
        }

        private void RestartLevel()
        {
            _gameWinOrLoseUIService.RestartLevel();
            _windowService.Close(Id);
        }
    }
}