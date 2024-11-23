using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View.Registrars;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    public class TileTweenAnimationRegistrar : EntityComponentRegistrar
    {
        public TileTweenAnimation TileTweenAnimation;
        
        public override void RegisterComponents()
        {
            Entity.AddTileTweenAnimation(TileTweenAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasTileTweenAnimation)
                Entity.RemoveTileTweenAnimation();
        }
    }
}