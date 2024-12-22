using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class TileTweenAnimation : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        
        private Vector3 _initialPosition;
        private Vector3 _initialScale;
        private Transform _startTransform;
        private GameObject _startGameObject;
        private Sequence _animationSequence;

        public void SetStartTransform()
        {
            transform.DOKill();

            if (_animationSequence != null)
            {
                _animationSequence.Kill();
                _animationSequence = null;
                // transform.position = _initialPosition;
                // transform.localScale = _initialScale;
                // transform.DOMove(_initialPosition, 0.1f).SetEase(Ease.Linear);
                transform.DOScale(_initialScale, 0.1f).SetEase(Ease.Linear);
            }
        }
        
        public void SaveTransform()
        {
            if (_animationSequence == null)
            {

            }
            _initialPosition = transform.position;
            _initialScale = transform.localScale;
        }

        public void ScaleChange(GameEntity entity)
        {
            SetStartTransform();
            
            GameObject tileGameObject = gameObject;
            Vector3 startPosition = tileGameObject.transform.position;
            Vector3 startScale = tileGameObject.transform.localScale;
            Vector3 finalPosition = startPosition;
            Vector3 overshootPosition = finalPosition + new Vector3(0, -0.05f, 0);
            Vector3 bouncePosition = finalPosition + new Vector3(0, 0.05f, 0);
            float duration = 0.2f;

            _animationSequence = DOTween.Sequence();

            _animationSequence.Append(tileGameObject.transform.DOMove(overshootPosition, duration / 2)
                .SetEase(Ease.OutQuad));

            _animationSequence.Join(tileGameObject.transform.DOScaleY(startScale.y * 0.8f, duration / 2)
                .SetEase(Ease.OutQuad));

            _animationSequence.Append(tileGameObject.transform.DOMove(bouncePosition, duration * 2 / 2)
                .SetEase(Ease.OutQuad));

            _animationSequence.Join(
                tileGameObject.transform.DOScaleY(startScale.y, duration / 2).SetEase(Ease.OutBounce));

            _animationSequence.Append(tileGameObject.transform.DOMove(finalPosition, duration / 2)
                .SetEase(Ease.OutQuad));

            _animationSequence.OnComplete(() =>
            {
                // entity.isFindMatches = true;
            });
        }

        public void MoveForCenterBooster(GameEntity entity, float scaleFactor = 2f)
        {
            // Сохраняем начальный масштаб объекта, уменьшенный на заданный коэффициент
            Vector3 targetScale = transform.localScale / scaleFactor;

            // Устанавливаем начальную позицию, если это необходимо
            SetStartTransform();

            // Создаем последовательность анимации
            Sequence sequence = DOTween.Sequence();

            // Анимация движения и уменьшения размера
            sequence.Append(transform.DOMove(entity.MovedForCenterBooster, 0.4f).SetEase(Ease.OutQuad))
                .Join(transform.DOScale(targetScale, 0.4f).SetEase(Ease.OutQuad)) // Одновременное уменьшение размера
                .OnComplete(() =>
                {
                    // Завершение анимации, если нужно выполнить дополнительные действия после
                    entity.isActiveInteraction = true;
                    // entity.isGoalCheck = true;
                    // entity.isDestructed = true;
                });

            // Устанавливаем флаг через 0.2 секунды независимо от завершения всей анимации
            DOVirtual.DelayedCall(0.2f, () =>
            {
                entity.isAnimationProcess = false;
            });
        }

        public void TilesOnDestroy(GameEntity entity)
        {
            // if (entity.hasTileTweenAnimation)
            // {
            //     entity.tileTweenAnimation.Value.SetStartTransform();
            // }

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad))/*
                .Append(transform.DOScale(Vector3.one * 0.1f, 0.1f).SetEase(Ease.OutQuad))*/
                .OnComplete(() =>
                {
                    // var particle = Instantiate(Particle, transform.parent);
                    // particle.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                    entity.isDestructed = true;
                });
        }

        public void SpawnPowerUp(GameEntity entity)
        {
            // Оригинальный конечный размер объекта
            Vector3 originalScale = transform.localScale;
            Quaternion originalRotation = transform.rotation;

            // Начальный масштаб, увеличенный на 1.2 от оригинала, и начальный поворот
            transform.localScale = originalScale * 1.2f;
            transform.rotation = Quaternion.Euler(0, 0, -12);

            // Получаем компонент SpriteRenderer и устанавливаем начальную прозрачность в 0
            // SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color originalColor = SpriteRenderer.color;
            originalColor.a = 0;
            SpriteRenderer.color = originalColor;

            Sequence sequence = DOTween.Sequence();

            // Добавляем анимации изменения масштаба, поворота и прозрачности
            sequence.Append(transform.DOScale(originalScale * 1.7f, 0.3f).SetEase(Ease.OutQuad)) // Увеличение до 1.5x за 0.2 сек
                .Join(transform.DORotate(new Vector3(0, 0, 12), 0.3f).SetEase(Ease.OutQuad))     // Поворот до +12 градусов за 0.2 сек
                .Join(SpriteRenderer.DOFade(1, 0.4f))                                            // Прозрачность до 100% за 0.2 сек
                
                .Append(transform.DOScale(originalScale * 0.9f, 0.2f).SetEase(Ease.OutQuad))     // Уменьшение до 0.9x за 0.4 сек
                .Join(transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutQuad))              // Возврат к 0 градусов за 0.4 сек
                
                .Append(transform.DOScale(originalScale, 0.05f).SetEase(Ease.OutQuad))            // Возврат к оригинальному масштабу за 0.3 сек
                .OnComplete(() =>
                {
                    entity.isAnimationProcess = false;
                    entity.isPowerUpSpawnAnimation = false;
                    
                    if (entity.isAutoActiveTile)
                    {
                        entity.isActiveInteraction = true;
                        // entity.isGoalCheck = true;
                    }
                });
        }


    }
}