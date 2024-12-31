using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(MagicBallTileAnimation))]
    public class MagicBallTileAnimationRegistrar : EntityComponentRegistrar
    {
        public BaseTileAnimation MagicBallTileAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (MagicBallTileAnimation == null) 
                MagicBallTileAnimation = GetComponentInChildren<BaseTileAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddBaseTileAnimation(MagicBallTileAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasBaseTileAnimation)
                Entity.RemoveBaseTileAnimation();
        }
    }
}