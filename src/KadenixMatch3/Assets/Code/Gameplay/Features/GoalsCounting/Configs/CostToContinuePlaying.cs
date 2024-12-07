using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.GoalsCounting.Configs
{
    [CreateAssetMenu(menuName = "ECS Survivors/CostToContinuePlayingConfigs", fileName = "CostToContinuePlayingConfigs")]
    public class CostToContinuePlayingConfig : ScriptableObject
    {
        public List<CostToContinuePlaying> CostToContinuePlayingsConfigs;
        public int CurrentNumberIteration = 0;
    }

    [Serializable]
    public class CostToContinuePlaying
    {
        public int Cost;
    }
}