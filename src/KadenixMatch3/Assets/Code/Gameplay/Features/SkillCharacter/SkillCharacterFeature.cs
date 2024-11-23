using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Gameplay.Features.SkillCharacter.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.SkillCharacter
{
    public sealed class SkillCharacterFeature : Feature
    {
        public SkillCharacterFeature(ISystemFactory systems)
        {
            Add(systems.Create<CharacterSkillActivationSystem>());
            
            Add(systems.Create<HandSkillSystem>());
            Add(systems.Create<SwapSkillSystem>());
        }
    }
}