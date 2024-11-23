using Code.Gameplay.Features.Tiles.Behaviours;
using Code.Infrastructure.View.Registrars;

namespace Code.Gameplay.Features.Tiles.Registrar
{
    public class PowerUpTileTweenAnimationRegistrar : EntityComponentRegistrar
    {
        public PowerUpTileTweenAnimation PowerUpTileTweenAnimation;
        
        public override void RegisterComponents()
        {
            Entity.AddPowerUpTileTweenAnimation(PowerUpTileTweenAnimation);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasPowerUpTileTweenAnimation)
                Entity.RemovePowerUpTileTweenAnimation();
        }
    }
}