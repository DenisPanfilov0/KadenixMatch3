namespace Code.Gameplay.Features.GoalsCounting.Services
{
    public interface IGameWinOrLoseUIService
    {
        void OpenLoseWindow();
        void OpenWinWindow();
        void RestartLevel();
        void EnterMainMenu();
        void Continue();
    }
}