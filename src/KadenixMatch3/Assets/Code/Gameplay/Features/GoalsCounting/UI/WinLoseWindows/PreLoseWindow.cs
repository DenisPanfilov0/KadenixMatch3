using System.Collections.Generic;
using Code.Gameplay.Features.GoalsCounting.Configs;
using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Windows;
using Code.Infrastructure.AssetManagement;
using Code.Meta.Feature.StartLevel.UI;
using Entitas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.GoalsCounting.UI.WinLoseWindows
{
    public class PreLoseWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _watchWideo;
        [SerializeField] private Button _payCoins;
        [SerializeField] private TextMeshProUGUI _payCoinsText;
        
        [SerializeField] private GoalMetaItem _goalMetaItem;
        [SerializeField] private Transform _goalsContainer;
        
        private IGameWinOrLoseUIService _gameWinOrLoseUIService;
        private IWindowService _windowService;
        private CostToContinuePlayingConfig _costToContinuePlayingConfig;
        private IGroup<GameEntity> _goals;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IGameWinOrLoseUIService gameWinOrLoseUIService, 
            IWindowService windowService, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _windowService = windowService;
            _gameWinOrLoseUIService = gameWinOrLoseUIService;
            Id = WindowId.ShopWindow;

            _goals = Contexts.sharedInstance.game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.GoalAmount, 
                    GameMatcher.GoalType)
                .NoneOf(
                    GameMatcher.GoalCompleted));

            Setup();
        }

        protected override void Initialize()
        {
            _backButton.onClick.AddListener(GameLose);
            _watchWideo.onClick.AddListener(WatchVideo);
            _payCoins.onClick.AddListener(PayCoins);
        }

        private void Setup()
        {
            _costToContinuePlayingConfig = _gameWinOrLoseUIService.CostToContinuePlayingConfig;

            _payCoins.interactable = _gameWinOrLoseUIService.IsItAvailablePurchase();

            if (_costToContinuePlayingConfig.CurrentNumberIteration > 0)
            {
                _watchWideo.gameObject.SetActive(false);
            }

            _payCoinsText.text = _costToContinuePlayingConfig.CostToContinuePlayingsConfigs[_costToContinuePlayingConfig.CurrentNumberIteration].Cost.ToString();
            
            foreach (GameEntity goal in _goals)
            {
                GoalMetaItem goalObject = Instantiate(_goalMetaItem, _goalsContainer);
                goalObject.Initialize(goal.GoalType.ToString(), goal.GoalAmount, _assetProvider);
            }
        }

        protected override void UnsubscribeUpdates()
        {
            _backButton.onClick.RemoveListener(GameLose);
            _watchWideo.onClick.RemoveListener(WatchVideo);
            _payCoins.onClick.RemoveListener(PayCoins);
        }

        private void GameLose()
        {
            _windowService.Close(Id);
            _gameWinOrLoseUIService.GameLose();
        }
        
        private void WatchVideo()
        {
            _windowService.Close(Id);
            _gameWinOrLoseUIService.AddMovesForVideo();
        }
        
        private void PayCoins()
        {
            _windowService.Close(Id);
            _gameWinOrLoseUIService.AddMovesForCoins();
        }
    }
}