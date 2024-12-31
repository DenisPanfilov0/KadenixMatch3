using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(PowerUpHorizontalRocketAnimation))]
    public class PowerUpHorizontalRocketAnimationRegistrar : EntityComponentRegistrar
    {
        public BaseTileAnimation PowerUpHorizontalRocketAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (PowerUpHorizontalRocketAnimation == null) 
                PowerUpHorizontalRocketAnimation = GetComponentInChildren<BaseTileAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddBaseTileAnimation(PowerUpHorizontalRocketAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasBaseTileAnimation)
                Entity.RemoveBaseTileAnimation();
        }
    }
}