using Entitas;

namespace Code.Gameplay.Features.CountingMoves
{
    [Game] public class Moves : IComponent { public int Value; }
    [Game] public class DecreaseMoves : IComponent { public int Value; }
    [Game] public class IncreaseMoves : IComponent { public int Value; }
    [Game] public class MovesChangeAmountProcess : IComponent { }
}