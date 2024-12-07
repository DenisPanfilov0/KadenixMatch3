using System.Collections.Generic;
using Code.Gameplay.Features.GoalsCounting.Configs;

namespace Code.Gameplay.Features.GoalsCounting.Services
{
    public interface IGameWinOrLoseUIService
    {
        void OpenLoseWindow();
        void OpenWinWindow();
        void RestartLevel();
        void EnterMainMenu();
        void Continue();
        void AddMovesForVideo();
        void AddMovesForCoins();
        void GameLose();
        CostToContinuePlayingConfig CostToContinuePlayingConfig { get; }
        bool IsItAvailablePurchase();
    }
}