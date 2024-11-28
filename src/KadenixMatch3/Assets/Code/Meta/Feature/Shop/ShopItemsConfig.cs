using System.Collections.Generic;
using UnityEngine;

namespace Code.Meta.Feature.Shop
{
    [CreateAssetMenu(fileName = "shopItemConfig", menuName = "ECS Survivors/Shop Item Config")]
    public class ShopItemsConfig : ScriptableObject
    {
        public List<ShopItemConfig> ShopItemConfigs;
    }
}