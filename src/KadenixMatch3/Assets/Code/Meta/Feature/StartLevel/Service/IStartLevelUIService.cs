using System;
using Code.Meta.Feature.Shop;

namespace Code.Meta.Feature.StartLevel.Service
{
    public interface IStartLevelUIService
    {
        int GetAmountItemShop(ShopItemId itemId);
        void OnClickButtonPreBooster(ShopItemId itemId);
        event Action<ShopItemId> PreBoosterSelected;
        void StartLevel();
    }
}