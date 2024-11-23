using Code.Gameplay.Features.BoardBuildFeature;
using Entitas;

namespace Code.Gameplay.Features.ActiveInteractionFeature
{
    [Game] public class ActiveInteraction : IComponent {  }
    [Game] public class InactiveStartState : IComponent {  }
        
    [Game] public class ColoredCrystal : IComponent {  }
    [Game] public class PowerUpVerticalRocket : IComponent {  }
    [Game] public class PowerUpHorizontalRocket : IComponent {  }
    [Game] public class PowerUpMagicalBall : IComponent {  }
    [Game] public class PowerUpBomb : IComponent {  }
    [Game] public class GrassModifier : IComponent {  }
    [Game] public class IceModifier : IComponent {  }
    
    [Game] public class PowerUpMagicalBallAndCrystal : IComponent { public TileTypeId Value; }
    [Game] public class PowerUpMagicalBallAndPowerUp : IComponent { public TileTypeId Value; }
    [Game] public class PowerUpBombAndBomb : IComponent { }
    [Game] public class PowerUpRocketAndRocket : IComponent { }
    [Game] public class PowerUpBombAndRocket : IComponent { }
    
    [Game] public class InteractionDelay : IComponent { public float Value; }
    [Game] public class InteractionStep : IComponent { public int Value; }
}