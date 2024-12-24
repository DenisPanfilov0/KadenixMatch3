namespace Code.Meta.Feature.Heart.Services
{
    public interface ILivesInfoService
    {
        bool GetAvailableStateBuyHeart();
        void BuyHeartForCoins();
        void WatchAd();
    }
}