using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class ColoredTileAnimation : MonoBehaviour
    {
        public void TilesOnDestroy(GameEntity entity, Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    entity.isDestructed = true;

                    if (callback != null)
                    {
                        callback?.Invoke();
                    }
                });
        }
    }
}