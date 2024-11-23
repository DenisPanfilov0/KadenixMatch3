using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.SkillCharacter.Systems
{
    public class CharacterSkillActivationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _skills;
        private List<GameEntity> _buffer = new(64);

        public CharacterSkillActivationSystem(GameContext game)
        {
            _skills = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.CharacterSkillActivationRequest)
                .NoneOf(GameMatcher.CharacterSkillProcessed));
        }
    
        public void Execute()
        {
            foreach (GameEntity skill in _skills.GetEntities(_buffer))
            {
                skill.isCharacterSkillProcessed = true;

                CreateEntity.Empty().With(x => x.isCanSwap = false);
            }
        }
    }
}