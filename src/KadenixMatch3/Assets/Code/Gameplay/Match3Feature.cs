using Code.Common.Destruct;
using Code.Gameplay.Features.ActiveInteractionFeature;
using Code.Gameplay.Features.ActiveStartStateTilesFeature;
using Code.Gameplay.Features.BindingTilesFeature;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.GoalsCounting;
using Code.Gameplay.Features.Input;
using Code.Gameplay.Features.PowerUpGeneratedFeature;
using Code.Gameplay.Features.SkillCharacter;
using Code.Gameplay.Features.TileGenerationFeature;
using Code.Gameplay.Features.TilesFallFeature;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
  public class Match3Feature : Feature
  {
    public Match3Feature(ISystemFactory systems)
    {
      Add(systems.Create<BoardBuildFeature>());
      
      Add(systems.Create<ActiveStartStateTilesFeature>());
      
      Add(systems.Create<TileGenerationFeature>());
      
      Add(systems.Create<BindViewFeature>());
      
      Add(systems.Create<InputFeature>());
      
      Add(systems.Create<FindMatchesFeature>());
      
      Add(systems.Create<PowerUpGeneratedFeature>());
      
      Add(systems.Create<ActiveInteractionFeature>());
      Add(systems.Create<GoalsCountingFeature>());
      
      Add(systems.Create<BindingTilesFeature>());
      Add(systems.Create<TilesFallFeature>());
      
      Add(systems.Create<SkillCharacterFeature>());
      
      Add(systems.Create<ProcessDestructedFeature>());
    }
  }
}