using System;
using UnityEngine;

namespace Code.Meta.Feature.Shop
{
    [Serializable]
    public class ShopItemConfig
    {
        public ShopItemId ShopItemId;
        public string Lable;
        public int Amount;
        public int Price;
        public string Description;
        public Sprite Icon;
    }
}