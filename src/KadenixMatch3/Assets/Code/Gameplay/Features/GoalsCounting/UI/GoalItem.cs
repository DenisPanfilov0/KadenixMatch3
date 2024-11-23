using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Infrastructure.AssetManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.GoalsCounting.UI
{
    public class GoalItem : MonoBehaviour
    {
        private TileTypeId _goalType;
        public TileTypeId GoalType => _goalType;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amount;
        
        private IAssetProvider _assetProvider;
        private int _amountRef;


        [Inject]
        public void Initialize(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void Setup(TileTypeId goalType, int amount)
        {
            GameObject loadAsset = await _assetProvider.Load<GameObject>(goalType.ToString());
            _icon.sprite = loadAsset.GetComponent<SpriteRenderer>().sprite;
            _amount.text = amount.ToString();
            _amountRef = amount;

            _goalType = goalType;
        }

        public void ChangeAmount(int amount)
        {
            _amountRef -= amount;
            _amount.text = _amountRef.ToString();
        }
    }
}