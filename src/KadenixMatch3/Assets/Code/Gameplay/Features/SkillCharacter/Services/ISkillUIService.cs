using System;

namespace Code.Gameplay.Features.SkillCharacter.Services
{
    public interface ISkillUIService
    {
        void UseHandSkill();
        void UseSwapSkill();
        void UseCrossDestructionSkill();
        event Action SkillAccept;
        void SkillAccepted();
        event Action SkillActive;
    }
}