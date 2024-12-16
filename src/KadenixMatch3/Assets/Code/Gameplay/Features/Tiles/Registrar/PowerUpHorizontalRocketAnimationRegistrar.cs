using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(PowerUpHorizontalRocketAnimation))]
    public class PowerUpHorizontalRocketAnimationRegistrar : EntityComponentRegistrar
    {
        public PowerUpHorizontalRocketAnimation PowerUpHorizontalRocketAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (PowerUpHorizontalRocketAnimation == null) 
                PowerUpHorizontalRocketAnimation = GetComponentInChildren<PowerUpHorizontalRocketAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddPowerUpHorizontalRocketAnimation(PowerUpHorizontalRocketAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasPowerUpHorizontalRocketAnimation)
                Entity.RemovePowerUpHorizontalRocketAnimation();
        }
    }
}