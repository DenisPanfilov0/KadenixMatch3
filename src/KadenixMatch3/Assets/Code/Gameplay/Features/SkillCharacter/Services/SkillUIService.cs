using System;
using Code.Common.Extensions;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.SkillCharacter.Services
{
    public class SkillUIService : ISkillUIService
    {
        public event Action SkillAccept;
        public event Action SkillActive;
        
        private readonly Contexts _contexts;

        [Inject]
        public SkillUIService(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void UseHandSkill()
        {
            var entity = _contexts.game.CreateEntity()
                .With(x => x.isCharacterSkillActivationRequest = true)
                .With(x => x.isHandSkillRequest = true);
            
            SkillActivated();
        }

        public void UseSwapSkill()
        {
            var entity = _contexts.game.CreateEntity()
                .With(x => x.isCharacterSkillActivationRequest = true)
                .With(x => x.isSwapSkillRequest = true);
            
            SkillActivated();
        }

        public void UseCrossDestructionSkill()
        {
            var entity = _contexts.game.CreateEntity()
                .With(x => x.isCharacterSkillActivationRequest = true)
                .With(x => x.isCrossDestructionSkillRequest = true);

            SkillActivated();
        }

        public void SkillAccepted()
        {
            SkillAccept?.Invoke();
        }        
        
        private void SkillActivated()
        {
            SkillActive?.Invoke();
        }
    }
}