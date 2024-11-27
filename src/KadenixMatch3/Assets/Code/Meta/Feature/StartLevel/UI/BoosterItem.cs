using System;
using System.Collections.Generic;
using Code.Meta.Feature.Shop;
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

        public void Initialize(IStartLevelUIService startLevelUIService)
        {
            _startLevelUIService = startLevelUIService;


            _amount = _startLevelUIService.GetAmountItemShop(_typeId);

            if (_amount > 0)
            {
                _amountText.text = _amount.ToString();
            }
            else if (_amount <= 0)
            {
                _amountText.gameObject.SetActive(false);
                _buyButton.gameObject.SetActive(true);
            }

            _button.onClick.AddListener(OnClickButtonPreBooster);
            _startLevelUIService.PreBoosterSelected += ChangeStatePreBooster;
            // _startLevelUIService.PreBoosterUnSelected += ChangeStatePreBooster;
        }

        private void OnClickButtonPreBooster()
        {
            _startLevelUIService.OnClickButtonPreBooster(_typeId);
        }

        private void ChangeStatePreBooster(ShopItemId itemId)
        {
            if (_typeId == itemId) 
                _backgroundSelected.gameObject.SetActive(!_backgroundSelected.gameObject.activeSelf);
        }

        private void OnDestroy()
        {
            _startLevelUIService.PreBoosterSelected -= ChangeStatePreBooster;
            // _startLevelUIService.PreBoosterUnSelected += ChangeStatePreBooster;
        }
    }
}