using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View.Registrars;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    public class GrassModifierAnimationRegistrar : EntityComponentRegistrar
    {
        public BaseTileAnimation GrassModifierAnimation;
        
        public override void RegisterComponents()
        {
            Entity.AddBaseTileAnimation(GrassModifierAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasBaseTileAnimation)
                Entity.RemoveBaseTileAnimation();
        }
    }
}