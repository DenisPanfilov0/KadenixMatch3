using UnityEngine;

namespace Code.Meta.Feature.Shop
{
    [CreateAssetMenu(fileName = "shopItemConfig", menuName = "ECS Survivors/Shop Item Config")]
    public class ShopItemConfig : ScriptableObject
    {
        public ShopItemId ShopItemId;
        public int Amount;
        public int Price;
        public string Description;
        public Sprite Icon;
    }
}