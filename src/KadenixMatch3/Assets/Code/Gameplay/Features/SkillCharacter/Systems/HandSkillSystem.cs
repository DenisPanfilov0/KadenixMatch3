using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.SkillCharacter.Services;
using Entitas;

namespace Code.Gameplay.Features.SkillCharacter.Systems
{
    public class HandSkillSystem : IExecuteSystem
    {
        private readonly ISkillUIService _skillUIService;
        private readonly IGroup<GameEntity> _skills;
        private readonly IGroup<InputEntity> _inputs;
        private readonly List<GameEntity> _buffer = new(64);
        private readonly List<InputEntity> _bufferInput = new(64);

        public HandSkillSystem(GameContext game, ISkillUIService skillUIService)
        {
            _skillUIService = skillUIService;
            _inputs = Contexts.sharedInstance.input.GetGroup(InputMatcher
                .AllOf(InputMatcher.SingleClick));            
            
            _skills = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.HandSkillRequest,
                    GameMatcher.CharacterSkillProcessed));
        }
    
        public void Execute()
        {
            foreach (InputEntity input in _inputs.GetEntities(_bufferInput))
            foreach (GameEntity skill in _skills.GetEntities(_buffer))
            {
                GameEntity entity = TileUtilsExtensions.GetTopTileByPosition(input.SingleClick);

                if (entity != null && !TileUtilsExtensions.GetTilesInCell(input.SingleClick).Any(x => x.isTileSpawner))
                {
                    entity.ReplaceDamageReceived(entity.DamageReceived + 1);
                    entity.isActiveInteraction = true;
                    entity.isGoalCheck = true;

                    skill.isCharacterSkillActivationRequest = false;
                    skill.isCharacterSkillProcessed = false;
                    skill.isHandSkillRequest = false;

                    skill.isDestructed = true;
                    input.RemoveSingleClick();
                    
                    _skillUIService.SkillAccepted();
                }
            }
        }
    }
}