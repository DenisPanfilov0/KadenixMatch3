using System;
using Code.Gameplay.Windows;

namespace Code.Meta.Feature.Shop.Services
{
    public interface IShopItemUIService
    {
        ShopItemConfig CurrentShopItemConfig { get; }
        void OpenShopItemWindow(ShopItemId typeId);
        void CloseWindow(WindowId id);
        void BuyItem(ShopItemConfig config, WindowId id);
        event Action/*<ShopItemId> */ItemPurchased;
    }
}