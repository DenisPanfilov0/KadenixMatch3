using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;

namespace Code.Gameplay.Features.PowerUpGeneratedFeature.Systems
{
    public class PowerUpGeneratedByTypeSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _powerUpsGenerated;
        private List<GameEntity> _buffer = new(64);

        public PowerUpGeneratedByTypeSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _powerUpsGenerated = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.TileForPowerUpGenerationByType));
        }
    
        public void Execute()
        {
            foreach (GameEntity powerUpGenerated in _powerUpsGenerated.GetEntities(_buffer))
            {
                GameEntity powerUp = _tileFactory.CreateTile(
                    powerUpGenerated.TileForPowerUpGenerationByType,
                    powerUpGenerated.WorldPosition, powerUpGenerated.BoardPosition, 
                    TileUtilsExtensions.GetTopTileByPosition(powerUpGenerated.BoardPosition).PositionInCoverageQueue + 1);

                powerUp.isAnimationProcess = true;
                powerUp.isNotMovable = true;
                powerUp.isMovable = false;
                powerUp.isAutoActiveTile = true;

                powerUpGenerated.RemoveTileForPowerUpGenerationByType();
            }
        }
    }
}