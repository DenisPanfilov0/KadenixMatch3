using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(PowerUpVerticalRocketAnimation))]
    public class PowerUpVerticalRocketAnimationRegistrar : EntityComponentRegistrar
    {
        public PowerUpVerticalRocketAnimation PowerUpVerticalRocketAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (PowerUpVerticalRocketAnimation == null) 
                PowerUpVerticalRocketAnimation = GetComponentInChildren<PowerUpVerticalRocketAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddPowerUpVerticalRocketAnimation(PowerUpVerticalRocketAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasPowerUpVerticalRocketAnimation)
                Entity.RemovePowerUpVerticalRocketAnimation();
        }
    }
}