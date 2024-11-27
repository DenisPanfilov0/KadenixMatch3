using System;
using System.Collections.Generic;
using Code.Meta.Feature.Shop;
using Code.Progress.Provider;
using UnityEngine;

namespace Code.Meta.Feature.StartLevel.Service
{
    public class StartLevelUIService : IStartLevelUIService
    {
        public event Action<ShopItemId> PreBoosterSelected;
        
        private readonly IProgressProvider _progress;
        private Dictionary<ShopItemId, int> _shopItemParser;

        private List<ShopItemId> _boostersSelected = new();

        public StartLevelUIService(IProgressProvider progress)
        {
            _progress = progress;
            
            _shopItemParser = new()
            {
                { ShopItemId.HandSkill, _progress.ProgressData.ProgressModel.CharacterBoosters.HandSkill},
                { ShopItemId.SwapSkill, _progress.ProgressData.ProgressModel.CharacterBoosters.SwapSkill}
            };
        }

        public int GetAmountItemShop(ShopItemId itemId)
        {
            _shopItemParser.TryGetValue(itemId, out int amount);
            return amount;
        }

        public void OnClickButtonPreBooster(ShopItemId itemId)
        {
            if (GetAmountItemShop(itemId) > 0)
            {
                if (_boostersSelected.Contains(itemId))
                {
                    _boostersSelected.Add(itemId);
                }
                else
                {
                    _boostersSelected.Remove(itemId);
                }
                
                PreBoosterSelected?.Invoke(itemId);
            }
            else
            {
                Debug.Log($"Open Shop {itemId}");
            }
        }
    }
}