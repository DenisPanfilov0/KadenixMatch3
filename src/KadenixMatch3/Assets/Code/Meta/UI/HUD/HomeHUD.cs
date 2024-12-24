using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Provider;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD
{
    public class HomeHUD : MonoBehaviour
    {
        private const string BattleSceneName = "Match3";
    
        private IGameStateMachine _stateMachine;
        private IWindowService _windowService;

        public Button StartBattleButton;
        private IProgressProvider _progress;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IWindowService windowService, IProgressProvider progress)
        {
            _progress = progress;
            _stateMachine = gameStateMachine;
            _windowService = windowService;
        }

        private void Awake()
        {
            StartBattleButton.onClick.AddListener(EnterBattleLoadingState);
        }

        private void EnterBattleLoadingState()
        {
            // _stateMachine.Enter<LoadingMatch3State, string>(BattleSceneName);

            if (_progress.ProgressData.ProgressModel.Heart > 0)
            {
                _windowService.Open(WindowId.StartLevelPanel);
            }
            else
            {
                _windowService.Open(WindowId.LivesInfoWindow);
            }
            
        }

        private void OpenShop()
        {
            _windowService.Open(WindowId.ShopWindow);
        }
    }
}