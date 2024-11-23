using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FindMatchesFeature
{
    [Game] public class FindMatches : IComponent { }
    [Game] public class IdenticalTilesForMatche : IComponent { public List<Vector2Int> Value; }
    [Game] public class TileGroupConvergeForBooster : IComponent { }
    [Game] public class FindMatchesProcess : IComponent { }
    [Game] public class SelectTileResearchMatches : IComponent { }
    
    [Game] public class MovedForCenterBooster : IComponent { public Vector3 Value; }
    
    [Game] public class TilesInShape : IComponent { public List<int> Value; }
}