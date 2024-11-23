using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TilesFallFeature.Systems
{
    public class FallAccelerationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tiles;
        private List<GameEntity> _buffer = new (64);

        public FallAccelerationSystem(GameContext tilesContext)
        {
            _tiles = tilesContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.StartedFalling,
                    GameMatcher.FallingSpeed,
                    GameMatcher.AccelerationTime));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _tiles.GetEntities(_buffer))
            {
                if (tile.AccelerationTime > 0 && tile.isProcessedFalling)
                {
                    tile.accelerationTime.Value -= 0.02f;
                    tile.fallingSpeed.Value += 0.3f;
                }
                else if (tile.AccelerationTime < 0 && !tile.isProcessedFalling)
                {
                    tile.ReplaceFallingSpeed(4f);
                    tile.ReplaceAccelerationTime(0.2f);
                    tile.isStartedFalling = false;
                }
            }
        }
    }
    
    public class DelayReductionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tiles;
        private List<GameEntity> _buffer = new (64);

        public DelayReductionSystem(GameContext tilesContext)
        {
            _tiles = tilesContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.StartedFalling,
                    GameMatcher.FallDelay));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _tiles.GetEntities(_buffer))
            {
                if (tile.FallDelay > 0)
                {
                    tile.ReplaceFallDelay(tile.FallDelay - 0.1f);
                }
                else
                {
                    tile.isStartedFalling = false;
                    tile.RemoveFallDelay();
                }
                // else if (tile.FallDelay < 0 && !tile.isProcessedFalling)
                // {
                //     tile.ReplaceFallDelay(0f);
                //     tile.isStartedFalling = false;
                // }
                // else if (tile.FallDelay < 0 && tile.isProcessedFalling)
                // {
                //     tile.isStartedFalling = false;
                // }
            }
        }
    }
}