using System;

namespace Code.Meta.Feature.Gold.Services
{
    public interface ICharacterGoldUIService
    {
        int GetCountCoins();
        void IncreaseGold(int count);
        event Action GoldChange;
        void DecreaseGold(int count);
    }
}