using System;
using Code.Gameplay.Features.GoalsCounting.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class PowerUpBombAnimation : BaseTileAnimation
    {
        [SerializeField] private ParticleSystem _explosionPartical;
        
        private IGoalsUIService _goalsUIService;

        [Inject]
        public void Construct(IGoalsUIService goalsUIService)
        {
            _goalsUIService = goalsUIService;
        }
        
        public override void TilesOnDestroy(GameEntity entity, Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();

            ParticleSystem particle = Instantiate(_explosionPartical, transform.parent);
            particle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);

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
    }
}