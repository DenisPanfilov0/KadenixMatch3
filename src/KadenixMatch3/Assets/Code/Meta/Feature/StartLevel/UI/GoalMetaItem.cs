using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.Feature.StartLevel.UI
{
    public class GoalMetaItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amount;

        public async void Initialize(KeyValuePair<string, int> goal, IAssetProvider assetProvider)
        {
            _amount.text = goal.Value.ToString();
            GameObject prefab = await assetProvider.Load<GameObject>(goal.Key);
            _icon.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
        
        public async void Initialize(string goalType, int goalCount, IAssetProvider assetProvider)
        {
            _amount.text = goalCount.ToString();
            GameObject prefab = await assetProvider.Load<GameObject>(goalType);
            _icon.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}