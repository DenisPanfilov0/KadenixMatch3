using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View.Registrars;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    public class IceModifierAnimationRegistrar : EntityComponentRegistrar
    {
        public BaseTileAnimation IceModifierAnimation;
        
        public override void RegisterComponents()
        {
            Entity.AddBaseTileAnimation(IceModifierAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasBaseTileAnimation)
                Entity.RemoveBaseTileAnimation();
        }
    }
}