using System.Collections.Generic;
using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class PowerUpTileTweenAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bombAndBombParticle;
        [SerializeField] private ParticleSystem _rocketAndBombParticle;
        [SerializeField] private ParticleSystem _rocketAndRocketParticle;

        private Vector3 _currentTransform;
        
        public void MagicBallAndCrystalActiveAnimation(GameEntity firstTileEntity, GameEntity secondTileEntity)
        {
            var firstTileTransform = firstTileEntity.Transform;
            var secondTileTransform = secondTileEntity.Transform;
            
            firstTileEntity.isTileActiveProcess = true;
            secondTileEntity.isTileActiveProcess = true;

            // Логика для первого тайла
            Vector3 firstTileCurrentPosition = firstTileTransform.position;
            Vector3 firstTileTargetPosition = secondTileTransform.position;
            Vector3 firstTileOffset = new Vector3(
                firstTileTargetPosition.x != firstTileCurrentPosition.x ? Mathf.Sign(firstTileTargetPosition.x - firstTileCurrentPosition.x) * 0.8f : 0f,
                firstTileTargetPosition.y != firstTileCurrentPosition.y ? Mathf.Sign(firstTileTargetPosition.y - firstTileCurrentPosition.y) * 0.8f : 0f,
                0f
            );

            Vector3 firstTileMoveTo = firstTileTargetPosition + firstTileOffset;

            // Анимация первого тайла: двигаем к позиции со смещением, затем к целевой позиции
            firstTileTransform.DOKill();
            firstTileTransform.DOMove(firstTileMoveTo, 0.3f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    firstTileTransform.DOMove(firstTileTargetPosition, 0.3f).SetEase(Ease.InOutQuad);
                });

            // Логика для второго тайла
            Vector3 secondTileCurrentPosition = secondTileTransform.position;
            Vector3 secondTileTargetPosition = firstTileTransform.position;
            Vector3 secondTileOffset = new Vector3(
                secondTileTargetPosition.x != secondTileCurrentPosition.x ? Mathf.Sign(secondTileTargetPosition.x - secondTileCurrentPosition.x) * 0.8f : 0f,
                secondTileTargetPosition.y != secondTileCurrentPosition.y ? Mathf.Sign(secondTileTargetPosition.y - secondTileCurrentPosition.y) * 0.8f : 0f,
                0f
            );

            Vector3 secondTileMoveTo = secondTileTargetPosition + secondTileOffset;

            // Анимация второго тайла: двигаем к позиции со смещением, затем к целевой позиции
            secondTileTransform.DOKill();
            secondTileTransform.DOMove(secondTileMoveTo, 0.3f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    secondTileTransform.DOMove(secondTileTargetPosition, 0.3f).SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            // Сбрасываем флаги анимации
                            // firstTileEntity.isAnimationProcess = false;
                            firstTileEntity.isFirstSelectPowerUpSwipe = false;
                            firstTileEntity.isSecondSelectPowerUpSwipe = false;
                            
                            // secondTileEntity.isAnimationProcess = false;
                            secondTileEntity.isFirstSelectPowerUpSwipe = false;
                            secondTileEntity.isSecondSelectPowerUpSwipe = false;

                            firstTileEntity.AddPowerUpMagicalBallAndCrystal(secondTileEntity.TileType);
                            firstTileEntity.isActiveInteraction = true;

                            // firstTileEntity.isAnimationProcess = false;
                            secondTileEntity.isAnimationProcess = false;
                            
                            secondTileEntity.isTileActiveProcess = false;
                            firstTileEntity.isTileActiveProcess = false;
                        });
                });
        }
        
        public void MagicBallAndPowerUpActiveAnimation(List<GameEntity> crystalTiles, GameEntity firstTileEntity, GameEntity secondTileEntity)
        {
            var firstTileTransform = firstTileEntity.Transform;
            var secondTileTransform = secondTileEntity.Transform;
            
            firstTileEntity.isTileActiveProcess = true;
            secondTileEntity.isTileActiveProcess = true;

            // Вычисляем начальные позиции
            Vector3 firstTileCurrentPosition = firstTileTransform.position;
            Vector3 secondTileCurrentPosition = secondTileTransform.position;

            // Находим точку ровно посередине между двумя тайлами
            Vector3 midpointPosition = (firstTileCurrentPosition + secondTileCurrentPosition) / 2;

            // Устанавливаем длительность анимации
            float moveDuration = 0.5f;

            // Анимация движения первого тайла к точке на полпути
            firstTileTransform.DOKill();
            firstTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Анимация движения второго тайла к той же точке на полпути
            secondTileTransform.DOKill();
            secondTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    // Логика завершения анимации, например, обновление состояния
                    firstTileEntity.isFirstSelectPowerUpSwipe = false;
                    firstTileEntity.isSecondSelectPowerUpSwipe = false;
                    secondTileEntity.isFirstSelectPowerUpSwipe = false;
                    secondTileEntity.isSecondSelectPowerUpSwipe = false;

                    // Активируем эффект взаимодействия
                    // firstTileEntity.AddPowerUpMagicalBallAndCrystal(secondTileEntity.TileType);


                    // Запускаем поочередную активацию тайлов из списка crystalTiles с задержкой 0.1 секунды
                    Sequence activationSequence = DOTween.Sequence();

                    foreach (var tile in crystalTiles)
                    {
                        activationSequence.AppendCallback(() =>
                        {
                            tile.isActiveInteraction = true;

                            if (secondTileEntity.TileType == TileTypeId.powerUpVerticalRocket || secondTileEntity.TileType == TileTypeId.powerUpHorizontalRocket)
                            {
                                int range = Random.Range(0, 2);
                                tile.AddTileForPowerUpGenerationByType(range == 1 ? TileTypeId.powerUpVerticalRocket : TileTypeId.powerUpHorizontalRocket);
                            }
                            else
                            {
                                tile.AddTileForPowerUpGenerationByType(secondTileEntity.TileType);
                            }
                            
                            tile.isGoalCheck = true;
                            tile.isActiveInteraction = true;
                        });
                        
                        activationSequence.AppendInterval(0.02f);
                    }
                    
                    // После активации всех тайлов помечаем firstTileEntity и secondTileEntity как удаленные
                    activationSequence.OnComplete(() =>
                    {
                        firstTileEntity.isDestructed = true;
                        secondTileEntity.isDestructed = true;
                    });
                });
        }
        
        public void MagicBallAndMagicBallActiveAnimation(List<GameEntity> tilesActivation, GameEntity firstTileEntity, GameEntity secondTileEntity)
        {
            var firstTileTransform = firstTileEntity.Transform;
            var secondTileTransform = secondTileEntity.Transform;
            
            firstTileEntity.isTileActiveProcess = true;
            secondTileEntity.isTileActiveProcess = true;

            // Вычисляем начальные позиции
            Vector3 firstTileCurrentPosition = firstTileTransform.position;
            Vector3 secondTileCurrentPosition = secondTileTransform.position;

            // Находим точку ровно посередине между двумя тайлами
            Vector3 midpointPosition = (firstTileCurrentPosition + secondTileCurrentPosition) / 2;

            // Устанавливаем длительность анимации
            float moveDuration = 0.5f;

            // Анимация движения первого тайла к точке на полпути
            firstTileTransform.DOKill();
            firstTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Анимация движения второго тайла к той же точке на полпути
            secondTileTransform.DOKill();
            secondTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    // Логика завершения анимации, например, обновление состояния
                    firstTileEntity.isFirstSelectPowerUpSwipe = false;
                    firstTileEntity.isSecondSelectPowerUpSwipe = false;
                    secondTileEntity.isFirstSelectPowerUpSwipe = false;
                    secondTileEntity.isSecondSelectPowerUpSwipe = false;
                    
                    // Активируем эффект взаимодействия
                    // firstTileEntity.AddPowerUpMagicalBallAndCrystal(secondTileEntity.TileType);


                    // Запускаем поочередную активацию тайлов из списка crystalTiles с задержкой 0.1 секунды
                    Sequence activationSequence = DOTween.Sequence();

                    foreach (var tile in tilesActivation)
                    {
                            tile.isActiveInteraction = true;
                            // tile.AddTileForPowerUpGenerationByType(secondTileEntity.TileType);
                    }
                    
                    firstTileEntity.isDestructed = true;
                    secondTileEntity.isDestructed = true;
                });
        }
        
        public void BombAndBombActiveAnimation(GameEntity firstTileEntity, GameEntity secondTileEntity)
        {
            var firstTileTransform = firstTileEntity.Transform;
            var secondTileTransform = secondTileEntity.Transform;
            
            firstTileEntity.isTileActiveProcess = true;
            secondTileEntity.isTileActiveProcess = true;

            // Вычисляем начальные позиции
            Vector3 firstTileCurrentPosition = firstTileTransform.position;
            Vector3 secondTileCurrentPosition = secondTileTransform.position;

            // Находим точку ровно посередине между двумя тайлами
            Vector3 midpointPosition = (firstTileCurrentPosition + secondTileCurrentPosition) / 2;

            // Устанавливаем длительность анимации
            float moveDuration = 0.5f;

            // Анимация движения первого тайла к точке на полпути
            firstTileTransform.DOKill();
            firstTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Анимация движения второго тайла к той же точке на полпути
            secondTileTransform.DOKill();
            secondTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    if (firstTileEntity.isSecondSelectPowerUpSwipe)
                    {
                        firstTileEntity.isPowerUpBombAndBomb = true;
                        firstTileEntity.isActiveInteraction = true;
                        secondTileEntity.isDestructed = true;
                        
                        _currentTransform = new Vector3(firstTileEntity.BoardPosition.x, firstTileEntity.BoardPosition.y, -3f);
                        
                    }
                    else if (secondTileEntity.isSecondSelectPowerUpSwipe)
                    {
                        secondTileEntity.isPowerUpBombAndBomb = true;
                        secondTileEntity.isActiveInteraction = true;
                        firstTileEntity.isDestructed = true;
                        
                        _currentTransform = new Vector3(secondTileEntity.BoardPosition.x, secondTileEntity.BoardPosition.y, -3f);
                    }
                    // Логика завершения анимации, например, обновление состояния
                    firstTileEntity.isFirstSelectPowerUpSwipe = false;
                    firstTileEntity.isSecondSelectPowerUpSwipe = false;
                    secondTileEntity.isFirstSelectPowerUpSwipe = false;
                    secondTileEntity.isSecondSelectPowerUpSwipe = false;
                    
                    firstTileEntity.isDestructed = true;
                    secondTileEntity.isDestructed = true;
                    
                    ParticleSystem particle = Instantiate(_bombAndBombParticle, transform.parent);
                    particle.transform.position = _currentTransform;
                });
        }

        public void RocketAndRocketActiveAnimation(GameEntity firstTileEntity, GameEntity secondTileEntity)
        {
            var firstTileTransform = firstTileEntity.Transform;
            var secondTileTransform = secondTileEntity.Transform;
            
            firstTileEntity.isTileActiveProcess = true;
            secondTileEntity.isTileActiveProcess = true;

            // Вычисляем начальные позиции
            Vector3 firstTileCurrentPosition = firstTileTransform.position;
            Vector3 secondTileCurrentPosition = secondTileTransform.position;

            // Находим точку ровно посередине между двумя тайлами
            Vector3 midpointPosition = (firstTileCurrentPosition + secondTileCurrentPosition) / 2;

            // Устанавливаем длительность анимации
            float moveDuration = 0.5f;

            // Анимация движения первого тайла к точке на полпути
            firstTileTransform.DOKill();
            firstTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Анимация движения второго тайла к той же точке на полпути
            secondTileTransform.DOKill();
            secondTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    if (firstTileEntity.isSecondSelectPowerUpSwipe)
                    {
                        firstTileEntity.isPowerUpRocketAndRocket = true;
                        firstTileEntity.isActiveInteraction = true;
                        secondTileEntity.isDestructed = true;
                        
                        _currentTransform = new Vector3(firstTileEntity.BoardPosition.x, firstTileEntity.BoardPosition.y, -3f);
                    }
                    else if (secondTileEntity.isSecondSelectPowerUpSwipe)
                    {
                        secondTileEntity.isPowerUpRocketAndRocket = true;
                        secondTileEntity.isActiveInteraction = true;
                        firstTileEntity.isDestructed = true;
                        
                        _currentTransform = new Vector3(secondTileEntity.BoardPosition.x, secondTileEntity.BoardPosition.y, -3f);
                    }
                    // Логика завершения анимации, например, обновление состояния
                    firstTileEntity.isFirstSelectPowerUpSwipe = false;
                    firstTileEntity.isSecondSelectPowerUpSwipe = false;
                    secondTileEntity.isFirstSelectPowerUpSwipe = false;
                    secondTileEntity.isSecondSelectPowerUpSwipe = false;

                    firstTileEntity.isDestructed = true;
                    secondTileEntity.isDestructed = true;
                    
                    ParticleSystem particle = Instantiate(_rocketAndRocketParticle, transform.parent);
                    particle.transform.position = _currentTransform;
                });
        }
        
        public void BombAndRocketActiveAnimation(GameEntity firstTileEntity, GameEntity secondTileEntity)
        {
            var firstTileTransform = firstTileEntity.Transform;
            var secondTileTransform = secondTileEntity.Transform;
            
            firstTileEntity.isTileActiveProcess = true;
            secondTileEntity.isTileActiveProcess = true;

            // Вычисляем начальные позиции
            Vector3 firstTileCurrentPosition = firstTileTransform.position;
            Vector3 secondTileCurrentPosition = secondTileTransform.position;

            // Находим точку ровно посередине между двумя тайлами
            Vector3 midpointPosition = (firstTileCurrentPosition + secondTileCurrentPosition) / 2;

            // Устанавливаем длительность анимации
            float moveDuration = 0.5f;

            // Анимация движения первого тайла к точке на полпути
            firstTileTransform.DOKill();
            firstTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Анимация движения второго тайла к той же точке на полпути
            secondTileTransform.DOKill();
            secondTileTransform.DOMove(midpointPosition, moveDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    if (firstTileEntity.isSecondSelectPowerUpSwipe)
                    {
                        firstTileEntity.isPowerUpBombAndRocket = true;
                        firstTileEntity.isActiveInteraction = true;
                        secondTileEntity.isDestructed = true;
                        
                        _currentTransform = new Vector3(firstTileEntity.BoardPosition.x, firstTileEntity.BoardPosition.y, -3f);
                    }
                    else if (secondTileEntity.isSecondSelectPowerUpSwipe)
                    {
                        secondTileEntity.isPowerUpBombAndRocket = true;
                        secondTileEntity.isActiveInteraction = true;
                        firstTileEntity.isDestructed = true;
                        
                        _currentTransform = new Vector3(secondTileEntity.BoardPosition.x, secondTileEntity.BoardPosition.y, -3f);
                    }
                    // Логика завершения анимации, например, обновление состояния
                    firstTileEntity.isFirstSelectPowerUpSwipe = false;
                    firstTileEntity.isSecondSelectPowerUpSwipe = false;
                    secondTileEntity.isFirstSelectPowerUpSwipe = false;
                    secondTileEntity.isSecondSelectPowerUpSwipe = false;

                    firstTileEntity.isDestructed = true;
                    secondTileEntity.isDestructed = true;
                    
                    ParticleSystem particle = Instantiate(_rocketAndBombParticle, transform.parent);
                    particle.transform.position = _currentTransform;
                });
        }
    }
}