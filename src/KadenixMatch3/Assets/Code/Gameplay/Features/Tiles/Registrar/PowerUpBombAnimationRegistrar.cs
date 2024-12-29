using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(PowerUpBombAnimation))]
    public class PowerUpBombAnimationRegistrar : EntityComponentRegistrar
    {
        public BaseTileAnimation PowerUpBombAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (PowerUpBombAnimation == null) 
                PowerUpBombAnimation = GetComponentInChildren<BaseTileAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddBaseTileAnimation(PowerUpBombAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasBaseTileAnimation)
                Entity.RemoveBaseTileAnimation();
        }
    }
}