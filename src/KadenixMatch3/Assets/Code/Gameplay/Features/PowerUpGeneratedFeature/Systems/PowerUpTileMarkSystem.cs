using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Entitas;

namespace Code.Gameplay.Features.PowerUpGeneratedFeature.Systems
{
    public class PowerUpTileMarkSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _powerUpsGenerated;
        private List<GameEntity> _buffer = new(64);

        public PowerUpTileMarkSystem(GameContext game)
        {
            _powerUpsGenerated = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Matchable,
                    GameMatcher.IdenticalTilesForMatche,
                    GameMatcher.PowerUpGenerated,
                    GameMatcher.PowerUpFigureType));
        }
    
        public void Execute()
        {
            foreach (GameEntity powerUpGenerated in _powerUpsGenerated.GetEntities(_buffer))
            {
                // foreach (var tilePosition in powerUpGenerated.IdenticalTilesForMatche)
                // {
                //     GameEntity tile = TileUtilsExtensions.GetTopTileByPosition(tilePosition);
                //     
                //     if (!tile.isBoardTile)
                //     {
                //         tile.isDestructed = true;
                //     }
                // }

                GameEntity entity = TileUtilsExtensions.GetBottomTileByPosition(powerUpGenerated.BoardPosition);
                entity.AddTileForPowerUpGeneration(powerUpGenerated.PowerUpFigureType);
                
                powerUpGenerated.RemoveIdenticalTilesForMatche();
            }
        }
    }
}