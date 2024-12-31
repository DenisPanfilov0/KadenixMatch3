using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(PowerUpVerticalRocketAnimation))]
    public class PowerUpVerticalRocketAnimationRegistrar : EntityComponentRegistrar
    {
        public BaseTileAnimation PowerUpVerticalRocketAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (PowerUpVerticalRocketAnimation == null) 
                PowerUpVerticalRocketAnimation = GetComponentInChildren<BaseTileAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddBaseTileAnimation(PowerUpVerticalRocketAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasBaseTileAnimation)
                Entity.RemoveBaseTileAnimation();
        }
    }
}