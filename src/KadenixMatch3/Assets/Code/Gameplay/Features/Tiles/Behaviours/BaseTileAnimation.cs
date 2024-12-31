using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Behaviours
{
    public class BaseTileAnimation : MonoBehaviour
    {
        public virtual void TilesOnDestroy(GameEntity entity, Action callback = null) { }
        public virtual void MoveTileToTarget(GameEntity entity, Action callback = null) { }
        public virtual void DurabilityChange(GameEntity entity) { }
        // public virtual void TileSwap(GameEntity entity, Vector3 direction) { }
        
        public virtual void TileSwap(GameEntity entity, Vector3 direction)
        {
            Vector3 targetPosition = transform.position + direction;

            transform.DOMove(targetPosition, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                entity.isTileSwipeFinished = true;
            });
        }
    }
}