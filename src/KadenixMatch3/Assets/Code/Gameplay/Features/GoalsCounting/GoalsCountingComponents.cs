using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.GoalsCounting
{
    [Game] public class GoalAmount : IComponent { public int Value; }
    [Game] public class GoalType : IComponent { public TileTypeId Value; }
    [Game] public class GoalCreateProcess : IComponent { }
    [Game] public class GoalMoving : IComponent { }
    
    [Game] public class GoalCheck : IComponent {  }
    [Game] public class GoalCompleted : IComponent {  }
    
    [Game] public class GameWin : IComponent {  }
    [Game] public class GameLose : IComponent {  }
}