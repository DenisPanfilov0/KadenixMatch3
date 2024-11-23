using System.Collections.Generic;

namespace Code.Gameplay.Features.FindMatchesFeature.Services
{
    public interface IFindMatchesService
    {
        Dictionary<List<GameEntity>, FigureTypeId> GetMatches(GameEntity entity);
        List<GameEntity> FindIdenticalTiles(GameEntity startTile);
    }
}