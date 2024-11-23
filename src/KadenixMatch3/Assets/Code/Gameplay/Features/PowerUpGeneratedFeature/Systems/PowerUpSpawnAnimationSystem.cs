using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;

namespace Code.Gameplay.Features.PowerUpGeneratedFeature.Systems
{
    public class PowerUpSpawnAnimationSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _powerUpsGenerated;
        private List<GameEntity> _buffer = new(64);

        public PowerUpSpawnAnimationSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _powerUpsGenerated = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpSpawnAnimation/*,
                    GameMatcher.View*/));
        }
    
        public void Execute()
        {
            foreach (GameEntity powerUpGenerated in _powerUpsGenerated.GetEntities(_buffer))
            {
                if (powerUpGenerated.hasTileTweenAnimation)
                {
                    powerUpGenerated.TileTweenAnimation.SpawnPowerUp(powerUpGenerated);
                    powerUpGenerated.isPowerUpSpawnAnimation = false;
                }
            }
        }
    }
}