using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.SkillCharacter
{
    [Game] public class CharacterSkillActivationRequest : IComponent {  }
    [Game] public class CharacterSkillProcessed : IComponent {  }
    
    [Game] public class HandSkillRequest : IComponent {  }
    [Game] public class SwapSkillRequest : IComponent {  }
    [Game] public class CrossDestructionSkillRequest : IComponent {  }
    
    [Game] public class TileNotActivatedAfterSwap : IComponent {  }
    [Game] public class TilesSkillSwap : IComponent { public List<Vector2Int> Value; }
}