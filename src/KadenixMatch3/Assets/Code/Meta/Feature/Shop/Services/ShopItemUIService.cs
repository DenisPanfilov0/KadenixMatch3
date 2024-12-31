using System;
using System.Collections.Generic;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using Code.Meta.Feature.StartLevel.Service;
using Code.Progress.Provider;

namespace Code.Meta.Feature.Shop.Services
{
    public class ShopItemUIService : IShopItemUIService
    {
        public event Action/*<ShopItemId>*/ ItemPurchased;
        public ShopItemConfig CurrentShopItemConfig => _currentShopItemConfig;
        
        private ShopItemConfig _currentShopItemConfig;
        
        private readonly IStartLevelUIService _startLevelUIService;
        private readonly IStaticDataService _staticDataService;
        private readonly IWindowService _windowService;
        private readonly IProgressProvider _progress;
        private readonly List<ShopItemConfig> _shopItemConfigs = new();

        public ShopItemUIService(IStaticDataService staticDataService, IWindowService windowService, IProgressProvider progress)
        {
            _staticDataService = staticDataService;
            _windowService = windowService;
            _progress = progress;

            _shopItemConfigs = _staticDataService.GetShopItemsConfig();
        }


        public void OpenShopItemWindow(ShopItemId typeId)
        {
            _currentShopItemConfig = _shopItemConfigs.Find(x => x.ShopItemId == typeId);
            _windowService.Open(WindowId.ShopItemWindow);
        }

        public void CloseWindow(WindowId id)
        {
            _windowService.Close(id);
        }

        public void BuyItem(ShopItemConfig config, WindowId id)
        {
            if (_progress.ProgressData.ProgressModel.Coins >= config.Price)
            {
                _progress.ProgressData.ProgressModel.Coins -= config.Price;
                GetItemByTypeId(config);
                ItemPurchased?.Invoke(/*config.ShopItemId*/);
                CloseWindow(id);
            }
        }

        private void GetItemByTypeId(ShopItemConfig config)
        {
            switch (config.ShopItemId)
            {
                case ShopItemId.PreBoosterBomb:
                    _progress.ProgressData.ProgressModel.CharacterPreBoosters.PreBoosterBomb += config.Amount;
                    break;
                case ShopItemId.PreBoosterLinearLightning:
                    _progress.ProgressData.ProgressModel.CharacterPreBoosters.PreBoosterLinearLightning += config.Amount;
                    break;
                case ShopItemId.PreBoosterMagicBall:
                    _progress.ProgressData.ProgressModel.CharacterPreBoosters.PreBoosterMagicBall += config.Amount;
                    break;
                case ShopItemId.HandSkill:
                    _progress.ProgressData.ProgressModel.CharacterSkills.HandSkill += config.Amount;
                    break;                
                case ShopItemId.SwapSkill:
                    _progress.ProgressData.ProgressModel.CharacterSkills.SwapSkill += config.Amount;
                    break;                
                case ShopItemId.DamageAllBoardSkill:
                    _progress.ProgressData.ProgressModel.CharacterSkills.DamageAllBoardSkill += config.Amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}