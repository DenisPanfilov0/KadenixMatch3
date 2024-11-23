using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    [RequireComponent(typeof(ColoredTileAnimation))]
    public class ColoredTileAnimationRegistrar : EntityComponentRegistrar
    {
        public ColoredTileAnimation ColoredTileAnimation;
        
        private void OnValidate()
        {
            if (EntityView == null) 
                EntityView = GetComponentInChildren<EntityBehaviour>();
            
            if (ColoredTileAnimation == null) 
                ColoredTileAnimation = GetComponentInChildren<ColoredTileAnimation>();
        }
        
        public override void RegisterComponents()
        {
            Entity.AddColoredTileAnimation(ColoredTileAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasColoredTileAnimation)
                Entity.RemoveColoredTileAnimation();
        }
    }
}