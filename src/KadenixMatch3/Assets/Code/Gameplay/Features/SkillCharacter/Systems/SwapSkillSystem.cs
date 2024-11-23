using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.SkillCharacter.Services;
using Entitas;

namespace Code.Gameplay.Features.SkillCharacter.Systems
{
    public class SwapSkillSystem : IExecuteSystem
    {
        private readonly ISkillUIService _skillUIService;
        private readonly IGroup<GameEntity> _skills;
        private readonly List<GameEntity> _buffer = new(8);
        private readonly IGroup<GameEntity> _tilesSkillSwap;

        public SwapSkillSystem(GameContext game, ISkillUIService skillUIService)
        {
            _skillUIService = skillUIService;
            
            _tilesSkillSwap = game.GetGroup(GameMatcher
            .AllOf(GameMatcher.TilesSkillSwap));     
            
            _skills = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.SwapSkillRequest,
                    GameMatcher.CharacterSkillProcessed));
        }
    
        public void Execute()
        {
            GameEntity firstTile = null;
            GameEntity secondTile = null;

            if (_tilesSkillSwap.count == 1)
            {

                foreach (var tile in _tilesSkillSwap.GetEntities(_buffer))
                {
                    firstTile = TileUtilsExtensions.GetTopTileByPosition(tile.TilesSkillSwap[0]);
                    secondTile = TileUtilsExtensions.GetTopTileByPosition(tile.TilesSkillSwap[1]);

                    tile.RemoveTilesSkillSwap();
                    tile.isDestructed = true;
                }
            }
            else
            {
                return;
            }
            
            foreach (GameEntity skill in _skills.GetEntities(_buffer))
            {
                
                firstTile.isFirstSelectTileSwipe = true;
                secondTile.isSecondSelectTileSwipe = true;
                                
                firstTile.isTileNotActivatedAfterSwap = true;
                secondTile.isTileNotActivatedAfterSwap = true;
                
                skill.isCharacterSkillActivationRequest = false;
                skill.isCharacterSkillProcessed = false;
                skill.isSwapSkillRequest = false;

                skill.isDestructed = true;
                _skillUIService.SkillAccepted();
            }
        }
    }
}