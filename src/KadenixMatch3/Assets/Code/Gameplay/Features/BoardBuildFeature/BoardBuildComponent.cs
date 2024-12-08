using Entitas;

namespace Code.Gameplay.Features.BoardBuildFeature
{
    // [Game] public class CanFall : IComponent { public int Value; }
    
    [Game] public class MaskEmptyCellsToFill : IComponent { public int[,] Value; }
    [Game] public class ModifierMaskEmptyCellsToFill : IComponent { public int[,] Value; }
}