using Code.Gameplay.Features.GoalsCounting.Services;
using Code.Gameplay.Windows;
using Code.Meta.Feature.Heart.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.Feature.Heart.UI
{
    public class LivesInfoWindow : BaseWindow
    {
        [SerializeField] private Button _adsButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _closeButton;
        private IWindowService _windowService;
        private ILivesInfoService _livesInfoService;

        [Inject]
        public void Construct(IWindowService windowService, ILivesInfoService livesInfoService)
        {
            _livesInfoService = livesInfoService;
            _windowService = windowService;
            Id = WindowId.LivesInfoWindow;
        }
        
        protected override void Initialize()
        {
            _adsButton.onClick.AddListener(WatchAd);

            if (_livesInfoService.GetAvailableStateBuyHeart())
            {
                _buyButton.onClick.AddListener(BuyHeart);
            }
            else
            {
                _buyButton.interactable = false;
            }
            
            _closeButton.onClick.AddListener(CloseWindow);
        }

        protected override void UnsubscribeUpdates()
        {
            _adsButton.onClick.RemoveListener(WatchAd);
            _buyButton.onClick.RemoveListener(BuyHeart);
            _closeButton.onClick.RemoveListener(CloseWindow);
        }

        private void BuyHeart()
        {
            _livesInfoService.BuyHeartForCoins();
            CloseWindow();
        }

        private void WatchAd()
        {
            _livesInfoService.WatchAd();
            CloseWindow();
        }

        private void CloseWindow()
        {
            _windowService.Close(Id);
        }
    }
}