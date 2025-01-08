using System;
using System.Collections.Generic;
using Code.Gameplay.Features.GoalsCounting.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class IceModifierAnimation : BaseTileAnimation
    {
        public SpriteRenderer SpriteRenderer;
        public List<Sprite> Sprites;

        private IGoalsUIService _goalsUIService;

        [Inject]
        public void Construct(IGoalsUIService goalsUIService)
        {
            _goalsUIService = goalsUIService;
        }
        
        public override void DurabilityChange(GameEntity entity)
        {
            SpriteRenderer.sprite = Sprites[entity.TileDurability - 1];
        }

        public override void TilesOnDestroy(GameEntity entity, Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    entity.isDestructed = true;
                });
        }
        
        public override void MoveTileToTarget(GameEntity entity, Action callback = null)
        {
            
                    entity.isAnimationProcess = false;
                    entity.isDestructed = true;
                    callback?.Invoke();
        }
    }
}