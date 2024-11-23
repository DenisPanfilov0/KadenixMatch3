using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
    public class InputPowerUpSwapSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _firstSelectTiles;
        private readonly IGroup<GameEntity> _secondSelectTiles;
        private List<GameEntity> _buffer = new(4);
    
        public InputPowerUpSwapSystem(GameContext game)
        {
            _firstSelectTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.FirstSelectPowerUpSwipe).NoneOf(GameMatcher.AnimationProcess));
            _secondSelectTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.SecondSelectPowerUpSwipe).NoneOf(GameMatcher.AnimationProcess));
        }
    
        public void Execute()
        {
            foreach (var firstSelectTile in _firstSelectTiles.GetEntities(_buffer))
            foreach (var secondSelectTile in _secondSelectTiles.GetEntities(_buffer))
            {
                firstSelectTile.TileTweenAnimation.SetStartTransform();
                secondSelectTile.TileTweenAnimation.SetStartTransform();

                firstSelectTile.isAnimationProcess = true;
                secondSelectTile.isAnimationProcess = true;
                
                if (firstSelectTile.isPowerUpMagicalBall && secondSelectTile.isPowerUpMagicalBall)
                {
                    List<GameEntity> tiles = new();
                    
                    for (var x = 0; x < 13; x++)
                    {
                        for (var y = 0; y <= 13; y++)
                        {
                            var position = new Vector2Int(x, y);
                            var tileEntity = TileUtilsExtensions.GetTopTileByPosition(position);
            
                            if (tileEntity != null && tileEntity.isMovable && tileEntity.TileType != TileTypeId.powerUpMagicBall
                                && !TileUtilsExtensions.GetTilesInCell(position).Any(t => t.isTileSpawner))
                            {
                                tiles.Add(tileEntity);
                            }
                        }
                    }
                    
                    firstSelectTile.PowerUpTileTweenAnimation.MagicBallAndMagicBallActiveAnimation(tiles, firstSelectTile, secondSelectTile);
                }
                else if (firstSelectTile.isPowerUpMagicalBall && secondSelectTile.isTilePowerUp)
                {
                    firstSelectTile.PowerUpTileTweenAnimation.MagicBallAndPowerUpActiveAnimation(GetMaxedCrystalTiles(), firstSelectTile, secondSelectTile);
                }
                else if (secondSelectTile.isPowerUpMagicalBall && firstSelectTile.isTilePowerUp)
                {
                    secondSelectTile.PowerUpTileTweenAnimation.MagicBallAndPowerUpActiveAnimation(GetMaxedCrystalTiles(), secondSelectTile, firstSelectTile);
                }
                else if (firstSelectTile.isPowerUpMagicalBall && secondSelectTile.isColoredCrystal)
                {
                    firstSelectTile.PowerUpTileTweenAnimation.MagicBallAndCrystalActiveAnimation(firstSelectTile, secondSelectTile);
                }
                else if (secondSelectTile.isPowerUpMagicalBall && firstSelectTile.isColoredCrystal)
                {
                    secondSelectTile.PowerUpTileTweenAnimation.MagicBallAndCrystalActiveAnimation(secondSelectTile, firstSelectTile);
                }
                else if (firstSelectTile.isTilePowerUp && secondSelectTile.isTilePowerUp)
                {
                    string advancedTileType = TileTypeParserExtensions.AdvancedPowerUpTypeResolve(new List<TileTypeId>{firstSelectTile.TileType, secondSelectTile.TileType});
                    
                    if (advancedTileType == "powerUpBombAndBomb") 
                        firstSelectTile.PowerUpTileTweenAnimation.BombAndBombActiveAnimation(firstSelectTile, secondSelectTile);
                    
                    if (advancedTileType == "powerUpRocketAndRocket") 
                        firstSelectTile.PowerUpTileTweenAnimation.RocketAndRocketActiveAnimation(firstSelectTile, secondSelectTile);
                    
                    if (advancedTileType == "powerUpBombAndRocket") 
                        firstSelectTile.PowerUpTileTweenAnimation.BombAndRocketActiveAnimation(firstSelectTile, secondSelectTile);
                }
                
                return;
            }
        }

        private List<GameEntity> GetMaxedCrystalTiles()
        {
            // Словарь для хранения кристаллов по их типам
            Dictionary<TileTypeId, List<GameEntity>> crystalGroups = new()
            {
                { TileTypeId.coloredRed, new List<GameEntity>() },
                { TileTypeId.coloredBlue, new List<GameEntity>() },
                { TileTypeId.coloredYellow, new List<GameEntity>() },
                { TileTypeId.coloredGreen, new List<GameEntity>() },
                { TileTypeId.coloredPurple, new List<GameEntity>() }
            };

            for (var x = 0; x < 13; x++)
            {
                for (var y = 0; y <= 13; y++)
                {
                    var position = new Vector2Int(x, y);
                    var tileEntity = TileUtilsExtensions.GetTopTileByPosition(position);
            
                    if (tileEntity != null && tileEntity.isMovable && tileEntity.isColoredCrystal
                        && !TileUtilsExtensions.GetTilesInCell(position).Any(t => t.isTileSpawner))
                    {
                        // Добавляем кристалл в соответствующую группу по его типу
                        if (crystalGroups.ContainsKey(tileEntity.TileType))
                        {
                            crystalGroups[tileEntity.TileType].Add(tileEntity);
                        }
                    }
                }
            }

            // Находим группу кристаллов с максимальным количеством элементов
            List<GameEntity> maxCrystalGroup = crystalGroups
                .OrderByDescending(group => group.Value.Count)
                .First()
                .Value;

            return maxCrystalGroup;
        }
    }
}