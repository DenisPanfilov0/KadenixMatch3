using System;
using Code.Gameplay.Features.GoalsCounting.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class PowerUpBombAnimation : MonoBehaviour
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

            // Instantiate(_explosionPartical, transform);

            sequence.Append(transform.DOScale(Vector3.zero, 3f).SetEase(Ease.OutQuad))
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
    }
}