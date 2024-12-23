using System;
using Code.Gameplay.Features.SettingInGame.Services;
using Code.Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.SettingInGame.UI
{
    public class PrematureExit : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        private ISettingsInGameService _settingsInGameService;
        
        [Inject]
        public void Construct(ISettingsInGameService settingsInGameService)
        {
            _settingsInGameService = settingsInGameService;
            
            _exitButton.onClick.AddListener(OpenPrematureExitWindow);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(OpenPrematureExitWindow);
        }

        private void OpenPrematureExitWindow()
        {
            _settingsInGameService.OpenPrematureExitWindow();
        }
    }
}