using System;
using System.Collections.Generic;
using Code.Meta.Feature.Shop;
using Code.Meta.Feature.Shop.Services;
using Code.Meta.Feature.StartLevel.Service;
using Code.Progress.Provider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.Feature.StartLevel.UI
{
    public class BoosterItem : MonoBehaviour
    {
        [SerializeField] private ShopItemId _typeId;
        [SerializeField] private Button _button;
        [SerializeField] private Image _buyButton;
        [SerializeField] private Image _backgroundSelected;
        [SerializeField] private TextMeshProUGUI _amountText;

        private int _amount;
        private IStartLevelUIService _startLevelUIService;
        private IShopItemUIService _shopItemUIService;

        public void Initialize(IStartLevelUIService startLevelUIService, IShopItemUIService shopItemUIService)
        {
            _shopItemUIService = shopItemUIService;
            _startLevelUIService = startLevelUIService;


            UpdateStatePreBooster();

            _button.onClick.AddListener(OnClickButtonPreBooster);
            _startLevelUIService.PreBoosterSelected += ChangeStatePreBooster;
            _shopItemUIService.ItemPurchased += UpdateStatePreBooster;
            // _startLevelUIService.PreBoosterUnSelected += ChangeStatePreBooster;
        }

        private void UpdateStatePreBooster()
        {
            _amount = _startLevelUIService.GetAmountItemShop(_typeId);

            if (_amount > 0)
            {
                _amountText.text = _amount.ToString();
                _amountText.gameObject.SetActive(true);
                _buyButton.gameObject.SetActive(false);
            }
            else if (_amount <= 0)
            {
                _amountText.gameObject.SetActive(false);
                _buyButton.gameObject.SetActive(true);
            }
        }

        private void OnClickButtonPreBooster()
        {
            _startLevelUIService.OnClickButtonPreBooster(_typeId);
        }

        private void ChangeStatePreBooster(ShopItemId itemId)
        {
            UpdateStatePreBooster();
            
            if (_typeId == itemId) 
                _backgroundSelected.gameObject.SetActive(!_backgroundSelected.gameObject.activeSelf);
        }

        private void OnDestroy()
        {
            _startLevelUIService.PreBoosterSelected -= ChangeStatePreBooster;
            _shopItemUIService.ItemPurchased -= UpdateStatePreBooster;
            // _startLevelUIService.PreBoosterUnSelected += ChangeStatePreBooster;
        }
    }
}