using Code.Gameplay.Features.Tiles.Behaviours;
using Entitas;

namespace Code.Gameplay.Features.Tiles
{
    [Game] public class ColoredTileAnimationComponent : IComponent { public ColoredTileAnimation Value; }
    [Game] public class PowerUpHorizontalRocketAnimationComponent : IComponent { public PowerUpHorizontalRocketAnimation Value; }
    [Game] public class PowerUpVerticalRocketAnimationComponent : IComponent { public PowerUpVerticalRocketAnimation Value; }
    [Game] public class PowerUpBombAnimationComponent : IComponent { public PowerUpBombAnimation Value; }
}