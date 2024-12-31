using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class GrassModifierAnimation : BaseTileAnimation
    {
        public SpriteRenderer SpriteRenderer;
        public List<Sprite> Sprites;

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
    }
}