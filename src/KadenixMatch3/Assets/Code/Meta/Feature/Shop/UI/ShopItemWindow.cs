using System;
using Code.Gameplay.Windows;
using Code.Meta.Feature.Shop.Services;
using Code.Progress.Provider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.Feature.Shop.UI
{
    public class ShopItemWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _lable;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _decription;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _closeWindow;
        [SerializeField] private Button _buyItem;
        
        private IShopItemUIService _shopItemUIService;
        private ShopItemConfig _shopItemConfig;


        [Inject]
        public void Construct(IShopItemUIService shopItemUIService)
        {
            _shopItemUIService = shopItemUIService;
            Id = WindowId.ShopWindow;
        }

        protected override void Initialize()
        {
            _shopItemConfig = _shopItemUIService.CurrentShopItemConfig;

            _lable.text = _shopItemConfig.Lable;
            _amount.text = $"x{_shopItemConfig.Amount}";
            _icon.sprite = _shopItemConfig.Icon;
            _decription.text = _shopItemConfig.Description;
            _price.text = $"Купить за {_shopItemConfig.Price}";

            _closeWindow.onClick.AddListener(CloseWindow);
            _buyItem.onClick.AddListener(BuyItem);
        }

        private void CloseWindow()
        {
            _shopItemUIService.CloseWindow(Id);
        }

        private void BuyItem()
        {
            _shopItemUIService.BuyItem(_shopItemConfig, Id);
        }

        protected override void UnsubscribeUpdates()
        {
            _closeWindow.onClick.RemoveListener(CloseWindow);
            _buyItem.onClick.RemoveListener(BuyItem);
        }
    }
}