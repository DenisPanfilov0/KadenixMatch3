using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View.Registrars;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    public class GrassModifierAnimationRegistrar : EntityComponentRegistrar
    {
        public GrassModifierAnimation GrassModifierAnimation;
        
        public override void RegisterComponents()
        {
            Entity.AddGrassModifierAnimation(GrassModifierAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasGrassModifierAnimation)
                Entity.RemoveGrassModifierAnimation();
        }
    }
}