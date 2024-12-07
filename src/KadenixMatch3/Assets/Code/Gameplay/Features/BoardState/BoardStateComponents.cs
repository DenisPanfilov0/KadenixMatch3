using Entitas;

namespace Code.Gameplay.Features.BoardState
{
    // [Game] public class CanFall : IComponent { public int Value; }
    
    [Game] public class BoardState : IComponent { }
    [Game] public class CanSwap : IComponent { }
    [Game] public class NoMoves : IComponent { }
    [Game] public class BoardActiveInteraction : IComponent { }
    [Game] public class StopGame : IComponent { }
}