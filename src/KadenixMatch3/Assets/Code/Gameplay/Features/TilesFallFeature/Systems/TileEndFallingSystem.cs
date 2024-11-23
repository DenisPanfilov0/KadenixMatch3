using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TilesFallFeature.Systems
{
    public class TileEndFallingSystem : IExecuteSystem
    {
        private List<GameEntity> _buffer = new(4);
        private readonly IGroup<GameEntity> _tilesEndFalling;

        public TileEndFallingSystem(GameContext game)
        {
            _tilesEndFalling = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.EndFalling)
                .NoneOf(GameMatcher.TileSwipeProcessed));
        }
    
        public void Execute()
        {
            foreach (var tileEndFalling in _tilesEndFalling.GetEntities(_buffer))
            {
                if (tileEndFalling.hasTileTweenAnimation)
                {
                    tileEndFalling.TileTweenAnimation.SaveTransform();
                    tileEndFalling.TileTweenAnimation.ScaleChange(tileEndFalling);
                    tileEndFalling.isEndFalling = false;
                }
            }
        }
    }
}