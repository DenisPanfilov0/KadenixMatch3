using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.FindMatchesFeature;
using Entitas;

namespace Code.Gameplay.Features.PowerUpGeneratedFeature
{
    [Game] public class PowerUpGenerated : IComponent {  }
    
    [Game] public class TileForPowerUpGeneration : IComponent { public FigureTypeId Value; }
    [Game] public class PowerUpFigureType : IComponent { public FigureTypeId Value; }

    [Game] public class TileForPowerUpGenerationByType : IComponent { public TileTypeId Value; }
    [Game] public class AutoActiveTile : IComponent { }

    [Game] public class PowerUpSpawnAnimation : IComponent {  }
    
    
}