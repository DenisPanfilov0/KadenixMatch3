using System;
using Code.Meta.Feature.Heart.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.Feature.Heart.UI
{
    public class CharacterHeart : MonoBehaviour
    {
        [SerializeField] private Image _addHearts;
        [SerializeField] private TextMeshProUGUI _heartAmountText;

        private int _heartAmount;
        private ICharacterHeartUIService _characterHeartUIService;

        [Inject]
        public void Construct(ICharacterHeartUIService characterHeartUIService)
        {
            _characterHeartUIService = characterHeartUIService;

            _characterHeartUIService.HeartChange += UpdateHeartsState;

            UpdateHeartsState();
        }

        private void OnDestroy()
        {
            _characterHeartUIService.HeartChange -= UpdateHeartsState;
        }

        private void UpdateHeartsState()
        {
            _heartAmount = _characterHeartUIService.GetCountHearts();
            _heartAmountText.text = _heartAmount.ToString();
        }
    }
}