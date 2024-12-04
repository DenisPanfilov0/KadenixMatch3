using System;

namespace Code.Meta.Feature.Heart.Services
{
    public interface ICharacterHeartUIService
    {
        int GetCountHearts();
        void IncreaseHeart(int count);
        void DecreaseHeart(int count);
        event Action HeartChange;
    }
}