using System;
using System.Collections.Generic;
using Code.Meta.Feature.Shop;
using Code.Meta.Feature.Shop.Services;
using Code.Progress.Provider;

namespace Code.Meta.Feature.StartLevel.Service
{
    public class StartLevelUIService : IStartLevelUIService
    {
        public event Action<ShopItemId> PreBoosterSelected;
        
        private readonly IProgressProvider _progress;
        private Dictionary<ShopItemId, int> _shopItemParser;

        private List<ShopItemId> _boostersSelected = new();
        private IShopItemUIService _shopItemUIService;

        public StartLevelUIService(IProgressProvider progress, IShopItemUIService shopItemUIService)
        {
            _shopItemUIService = shopItemUIService;
            _progress = progress;
            
            ShopItemParserUpdate();

            // _shopItemUIService.ItemPurchased += OnClickButtonPreBooster;
        }

        private void ShopItemParserUpdate()
        {
            _shopItemParser = new()
            {
                { ShopItemId.HandSkill, _progress.ProgressData.ProgressModel.CharacterBoosters.HandSkill},
                { ShopItemId.SwapSkill, _progress.ProgressData.ProgressModel.CharacterBoosters.SwapSkill}
            };
        }

        public int GetAmountItemShop(ShopItemId itemId)
        {
            ShopItemParserUpdate();
            
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
                _shopItemUIService.OpenShopItemWindow(itemId);
            }
        }
    }
}