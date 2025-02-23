using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Windows;
using Code.Infrastructure.AssetManagement;
using Code.Meta.Feature.Shop.Services;
using Code.Meta.Feature.StartLevel.Service;
using Code.Progress.Data;
using Code.Progress.Provider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.Feature.StartLevel.UI
{
    public class StartLevelPanel : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _currentLevel;
        [SerializeField] private Transform _goalsContainer;
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private List<BoosterItem> _boosters;
        [SerializeField] private GoalMetaItem _goalMetaItem;
        
        private IProgressProvider _progress;
        private Level _lvl;
        private IAssetProvider _assetProvider;
        private IWindowService _windowService;
        private IStartLevelUIService _startLevelUIService;
        private IShopItemUIService _shopItemUIService;

        [Inject]
        public void Construct(IProgressProvider progress, IAssetProvider assetProvider, IWindowService windowService,
            IStartLevelUIService startLevelUIService, IShopItemUIService shopItemUIService)
        {
            _shopItemUIService = shopItemUIService;
            _startLevelUIService = startLevelUIService;
            _windowService = windowService;
            _assetProvider = assetProvider;
            _progress = progress;
            
            _lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);
            
            Id = WindowId.StartLevelPanel;
        }

        private void Start()
        {
            GoalsFill();
            BoostersUpdateState();
            
            _closeButton.onClick.AddListener(() => _windowService.Close(Id));
            _startLevelButton.onClick.AddListener(() =>
            {
                _startLevelUIService.StartLevel();
            });

        }

        public void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
        }

        private void GoalsFill()
        {
            foreach (KeyValuePair<string, int> goal in _lvl.goals)
            {
                GoalMetaItem goalObject = Instantiate(_goalMetaItem, _goalsContainer);
                goalObject.Initialize(goal, _assetProvider);
            }
        }

        private void BoostersUpdateState()
        {
            foreach (var booster in _boosters)
            {
                booster.Initialize(_startLevelUIService, _shopItemUIService);
            }
        }
    }
}