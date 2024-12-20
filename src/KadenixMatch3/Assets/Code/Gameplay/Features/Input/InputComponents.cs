using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Code.Gameplay.Features.Input
{
    [Input] public class SingleClick : IComponent { public Vector2Int Value; }
    [Input] public class DoubleClick : IComponent { public Vector2Int Value; }
    
    [Game, Unique] public class FirstSelectTileSwipe : IComponent {  }
    [Game, Unique] public class SecondSelectTileSwipe : IComponent {  }
    [Game, Unique] public class FirstSelectPowerUpSwipe : IComponent {  }
    [Game, Unique] public class SecondSelectPowerUpSwipe : IComponent {  }
    [Game, Unique] public class TileForDoubleClick : IComponent {  }
    
    [Game] public class TileSwipeProcessed : IComponent {  }
    [Game] public class SwipeDirection : IComponent { public Vector3 Value; }
}