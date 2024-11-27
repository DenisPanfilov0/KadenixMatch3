using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Infrastructure.AssetManagement;
using Code.Progress.Data;
using Code.Progress.Provider;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.GoalsCounting.UI
{
    public class GoalsPanel : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        
        private IAssetProvider _assetProvider;
        private IProgressProvider _progress;
        private IInstantiator _instantiator;
        private IGoalsUIService _goalsUIService;

        private List<GoalItem> _items = new();

        [Inject]
        public void Construct(IAssetProvider assetProvider, IProgressProvider progress, IInstantiator instantiator, IGoalsUIService goalsUIService)
        {
            _goalsUIService = goalsUIService;
            _instantiator = instantiator;
            _progress = progress;
            _assetProvider = assetProvider;
        }

        private void Start()
        {
            GoalsFill();

            _goalsUIService.OnChangeGoal += ChangeGoal;
        }

        private void OnDestroy()
        {
            _goalsUIService.OnChangeGoal -= ChangeGoal;
        }

        private async Task GoalsFill()
        {
            Level lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);

            foreach (var goal in lvl.goals)
            {
                GameObject goalPrefab = await _assetProvider.Load<GameObject>("goal");
                GoalItem goalItem = _instantiator.InstantiatePrefabForComponent<GoalItem>(goalPrefab, _container);
                goalItem.Setup(TileTypeParserExtensions.TileTypeResolve(goal.Key), goal.Value);
                _items.Add(goalItem);
            }
        }

        private void ChangeGoal(TileTypeId typeId, int amount)
        {
            foreach (var item in _items)
            {
                if (item.GoalType == typeId)
                {
                    item.ChangeAmount(amount);
                }
            }
        }
    }
}