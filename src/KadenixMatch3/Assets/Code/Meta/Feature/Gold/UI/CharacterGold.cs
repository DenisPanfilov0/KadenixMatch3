using System;
using Code.Meta.Feature.Gold.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.Feature.Gold.UI
{
    public class CharacterGold : MonoBehaviour
    {
        [SerializeField] private Image _addCoins;
        [SerializeField] private TextMeshProUGUI _coinAmountText;

        private int _coinAmount;
        private ICharacterGoldUIService _characterGoldUIService;

        [Inject]
        public void Construct(ICharacterGoldUIService characterGoldUIService)
        {
            _characterGoldUIService = characterGoldUIService;

            _characterGoldUIService.GoldChange += UpdateHeartsState;

            UpdateHeartsState();
        }

        private void UpdateHeartsState()
        {
            _coinAmount = _characterGoldUIService.GetCountCoins();
            _coinAmountText.text = _coinAmount.ToString();
        }

        private void OnDestroy()
        {
            _characterGoldUIService.GoldChange -= UpdateHeartsState;
        }
    }
}