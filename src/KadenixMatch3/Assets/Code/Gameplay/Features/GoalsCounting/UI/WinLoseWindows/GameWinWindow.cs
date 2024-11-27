using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.GoalsCounting.UI.WinLoseWindows
{
    public class GameWinWindow : BaseWindow
    {
        [SerializeField] private Button _continueButton;
        
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
            _continueButton.onClick.AddListener(Continue);
        }

        protected override void UnsubscribeUpdates()
        {
            _continueButton.onClick.RemoveListener(Continue);
        }

        private void Continue()
        {
            _windowService.Close(Id);
            _gameWinOrLoseUIService.Continue();
        }
    }
}