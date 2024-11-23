using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View.Registrars;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    public class IceModifierAnimationRegistrar : EntityComponentRegistrar
    {
        public IceModifierAnimation IceModifierAnimation;
        
        public override void RegisterComponents()
        {
            Entity.AddIceModifierAnimation(IceModifierAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasIceModifierAnimation)
                Entity.RemoveIceModifierAnimation();
        }
    }
}