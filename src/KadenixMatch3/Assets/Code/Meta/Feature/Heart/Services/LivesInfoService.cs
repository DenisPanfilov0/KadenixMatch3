using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.Feature.Gold.Services;
using Code.Progress.Provider;
using UnityEngine;

namespace Code.Meta.Feature.Heart.Services
{
    public class LivesInfoService : ILivesInfoService
    {
        private readonly IProgressProvider _progress;
        private readonly ICharacterGoldUIService _characterGoldUIService;
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterHeartUIService _characterHeartUIService;

        public LivesInfoService(IProgressProvider progress, ICharacterGoldUIService characterGoldUIService,
            IGameStateMachine stateMachine, ICharacterHeartUIService characterHeartUIService)
        {
            _stateMachine = stateMachine;
            _characterHeartUIService = characterHeartUIService;
            _characterGoldUIService = characterGoldUIService;
            _progress = progress;
        }

        public bool GetAvailableStateBuyHeart()
        {
            return _progress.ProgressData.ProgressModel.Coins >= 900;
        }

        public void BuyHeartForCoins()
        {
            _characterGoldUIService.DecreaseGold(900);
            _characterHeartUIService.IncreaseHeart(5);
            // EnterMainMenu();
        }

        public void WatchAd()
        {
            Debug.Log("Посмтрели рекламу" + UnityEngine.Time.time);
            _characterHeartUIService.IncreaseHeart(1);
            // EnterMainMenu();
        }

        // private void EnterMainMenu()
        // {
        //     _stateMachine.Enter<LoadingHomeScreenState>();
        // }
    }
}