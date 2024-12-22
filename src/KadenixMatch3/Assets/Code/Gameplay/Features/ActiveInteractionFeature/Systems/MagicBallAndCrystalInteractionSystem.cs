using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ActiveInteractionFeature.Systems
{
    public class MagicBallAndCrystalInteractionSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _tilesInteraction;
        private List<GameEntity> _buffer = new(64);

        public MagicBallAndCrystalInteractionSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _tilesInteraction = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PowerUpMagicalBallAndCrystal,
                    GameMatcher.ActiveInteraction)
                .NoneOf(GameMatcher.InteractionDelay/*, GameMatcher.AnimationProcess*/));
        }
    
        public void Execute()
        {
            foreach (GameEntity tileInteraction in _tilesInteraction.GetEntities(_buffer))
            {
                tileInteraction.isActiveInteraction = false;
                tileInteraction.isDestructed = true;
                
                List<GameEntity> tilesDirectInteraction = new();
                
                for (var x = 0; x < 13; x++)
                {
                    for (var y = 0; y <= 13; y++)
                    {
                        var position = new Vector2Int(x, y);
                        var tileEntities = TileUtilsExtensions.GetTopTileByPosition(position);
                        if (tileEntities != null && tileEntities.TileType == tileInteraction.PowerUpMagicalBallAndCrystal 
                            && !TileUtilsExtensions.GetTilesInCell(position).Any(x => x.isTileSpawner)
                            && !tileEntities.isActiveInteraction && !tileEntities.isBoardTile && !tileEntities.isTileActiveProcess)
                        {
                            tilesDirectInteraction.Add(tileEntities);
                        }
                    }
                }

                foreach (var tile in tilesDirectInteraction)
                {
                    tile.ReplaceDamageReceived(tile.DamageReceived + 1);
                    tile.isActiveInteraction = true;
                    // tile.isGoalCheck = true;
                }

                // tileInteraction.TileTweenAnimation.TilesOnDestroy(tileInteraction);
                // tileInteraction.isGoalCheck = true;
                
                tilesDirectInteraction.Clear();
            }
        }
    }
}