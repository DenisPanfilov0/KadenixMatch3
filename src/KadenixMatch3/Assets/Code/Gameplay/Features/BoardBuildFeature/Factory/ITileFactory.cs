using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Factory
{
    public interface ITileFactory
    {
        GameEntity CreateTile(TileTypeId typeId, Vector3 at, Vector2Int boardPosition, int positionInCoverageQueue = default, int durability = default);
    }
}