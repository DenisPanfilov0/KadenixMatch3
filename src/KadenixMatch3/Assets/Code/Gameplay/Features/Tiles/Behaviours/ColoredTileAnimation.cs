using System;
using Code.Gameplay.Features.GoalsCounting.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class ColoredTileAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionPartical;
        
        private IGoalsUIService _goalsUIService;

        [Inject]
        public void Construct(IGoalsUIService goalsUIService)
        {
            _goalsUIService = goalsUIService;
        }

        public void TilesOnDestroy(GameEntity entity, Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();

            ParticleSystem particle = Instantiate(_explosionPartical, transform.parent);
            particle.transform.position = transform.position;

            sequence.Append(transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    entity.isDestructed = true;
                    entity.isAnimationProcess = false;

                    if (callback != null)
                    {
                        callback?.Invoke();
                    }
                });
        }

        public void MoveTileToTarget(GameEntity entity, Action callback = null)
        {
            GetComponent<SpriteRenderer>().enabled = false;

            // entity.RemoveBoardPosition();
            
            // Получаем трансформ цели из UI
            Transform goalUITransform = _goalsUIService.GetGoalTransform(entity.TileType);
            if (goalUITransform == null)
            {
                Debug.LogError("Goal transform not found!");
                return;
            }

            // Преобразуем мировые координаты текущего объекта в экранные
            Vector3 startWorldPosition = transform.position;
            Vector2 startScreenPosition = Camera.main.WorldToScreenPoint(startWorldPosition);

            // Преобразуем мировые координаты цели в экранные с учетом канваса
            Vector2 goalScreenPosition = RectTransformUtility.WorldToScreenPoint(null, goalUITransform.position);

            // Создаем новый объект в UI
            GameObject imageObject = new GameObject("TileModifierImage");
            imageObject.transform.SetParent(goalUITransform.GetComponentInParent<Canvas>().transform, false);

            // Добавляем компонент Image
            Image image = imageObject.AddComponent<Image>();
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer not found on the object!");
                return;
            }

            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;

            // Устанавливаем начальное положение
            RectTransform imageRectTransform = image.rectTransform;
            imageRectTransform.position = startScreenPosition;

            // Рассчитываем расстояние для движения
            float distance = Vector2.Distance(startScreenPosition, goalScreenPosition);

            // Получаем коэффициент масштабирования для скорости, основанный на разрешении экрана
            float screenScaleFactor = Screen.width / 1920f; // 1920 — это базовое разрешение для скорости

            // Определяем базовую скорость и умножаем на коэффициент масштаба экрана
            float baseSpeed = 650f; // Базовая скорость
            float adjustedSpeed = baseSpeed * screenScaleFactor;

            // Рассчитываем длительность анимации
            float duration = distance / adjustedSpeed;

            // Создаем последовательность анимации
            Sequence animationSequence = DOTween.Sequence();

            Tween moveTween = imageRectTransform
                .DOMove(goalScreenPosition, duration)
                .SetEase(Ease.InBack);

            Tween scaleTween = imageRectTransform
                .DOScale(Vector3.one * 0.8f, duration)
                .SetEase(Ease.OutCubic);

            animationSequence
                .Append(moveTween)
                .Join(scaleTween)
                .OnComplete(() =>
                {
                    Destroy(imageObject);

                    entity.isAnimationProcess = false;
                    entity.isDestructed = true;
                    callback?.Invoke();
                });
        }
    }
}