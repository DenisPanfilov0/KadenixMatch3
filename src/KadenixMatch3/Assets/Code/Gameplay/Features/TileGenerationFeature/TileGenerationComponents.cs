using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature;
using Entitas;

namespace Code.Gameplay.Features.TileGenerationFeature
{
    [Game] public class SpawnerTilesPool : IComponent { public List<TileTypeId> Value; }
}