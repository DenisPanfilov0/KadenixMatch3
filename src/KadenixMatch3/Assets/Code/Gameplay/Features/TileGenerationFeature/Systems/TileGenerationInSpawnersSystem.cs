using Code.Gameplay.Common.Extension;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.BoardBuildFeature.Factory;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileGenerationFeature.Systems
{
    public sealed class TileGenerationInSpawnersSystem : IExecuteSystem
    {
        private readonly ITileFactory _tileFactory;
        private readonly IGroup<GameEntity> _spawners;

        public TileGenerationInSpawnersSystem(GameContext game, ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            
            _spawners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TileSpawner, 
                    GameMatcher.SpawnerTilesPool));
        }
        
        public void Execute()
        {
            foreach (var spawner in _spawners)
            {
                if (TileUtilsExtensions.GetTilesInCell(spawner.BoardPosition).Count == 1)
                {
                    _tileFactory.CreateTile(spawner.SpawnerTilesPool[Random.Range(0, spawner.SpawnerTilesPool.Count)], spawner.WorldPosition, spawner.BoardPosition, 0);
                }
            }
        }
    }
}