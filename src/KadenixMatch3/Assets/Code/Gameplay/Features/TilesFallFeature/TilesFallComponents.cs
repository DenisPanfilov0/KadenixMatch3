using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TilesFallFeature
{
    [Game] public class CanFall : IComponent {  }
    [Game] public class StartedFalling : IComponent {  }
    [Game] public class ProcessedFalling : IComponent {  }
    [Game] public class EndFalling : IComponent {  }
    [Game] public class SpeedFall : IComponent { public float Value; }
    [Game] public class FallDirection : IComponent { public Vector3 Value; }
    [Game] public class FallingSpeed : IComponent { public float Value; }
    [Game] public class AccelerationTime : IComponent { public float Value; }
    [Game] public class FallDelay : IComponent { public float Value; }
    // [Game] public class CanFall : IComponent { public int Value; }
}