using Code.Gameplay.Features.SkillCharacter.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.SkillCharacter.UI
{
    public class SkillsWindow : MonoBehaviour
    {
        public Button HandSkillButton;
        public Button SwapSkillButton;
        public Button CrossDestroySkillButton;

        private ISkillUIService _skillUIService;

        [Inject]
        private void Construct(ISkillUIService skillUIService)
        {
            _skillUIService = skillUIService;

            _skillUIService.SkillAccept += SkillChangeStateActive;
            _skillUIService.SkillActive += SkillChangeStateNoActive;
        }

        private void Start()
        {
            HandSkillButton.onClick.AddListener(() => _skillUIService.UseHandSkill());
            SwapSkillButton.onClick.AddListener(() => _skillUIService.UseSwapSkill());
            CrossDestroySkillButton.onClick.AddListener(() => _skillUIService.UseCrossDestructionSkill());
        }

        private void OnDestroy()
        {
            HandSkillButton.onClick.RemoveAllListeners();
            SwapSkillButton.onClick.RemoveAllListeners();
            CrossDestroySkillButton.onClick.RemoveAllListeners();
            
            _skillUIService.SkillAccept -= SkillChangeStateActive;
            _skillUIService.SkillActive -= SkillChangeStateNoActive;
        }

        private void SkillChangeStateActive()
        {
            HandSkillButton.interactable = true;
            SwapSkillButton.interactable = true;
            CrossDestroySkillButton.interactable = true;
        }
        
        private void SkillChangeStateNoActive()
        {
            HandSkillButton.interactable = false;
            SwapSkillButton.interactable = false;
            CrossDestroySkillButton.interactable = false;
        }
    }
}