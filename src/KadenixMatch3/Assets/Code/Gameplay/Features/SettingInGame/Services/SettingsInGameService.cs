using Code.Gameplay.Windows;

namespace Code.Gameplay.Features.SettingInGame.Services
{
    public class SettingsInGameService : ISettingsInGameService
    {
        private readonly IWindowService _windowService;

        public SettingsInGameService(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void OpenPrematureExitWindow()
        {
            _windowService.Open(WindowId.PrematureExitWindow);
        }
    }
}