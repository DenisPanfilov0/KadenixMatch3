using Code.Gameplay.Features.BoardBuildFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.BoardBuildFeature
{
    public sealed class BoardBuildFeature : Feature
    {
        public BoardBuildFeature(ISystemFactory systems)
        {
            Add(systems.Create<BoardTileCreateSystem>());
            Add(systems.Create<TileModifierCreateSystem>());
            Add(systems.Create<TileContentCreateSystem>());
            Add(systems.Create<TileContentModifierCreateSystem>());
            
            Add(systems.Create<EmptyCellSearchSystem>());
            Add(systems.Create<EmptyCellFillingSystem>());
        }
    }
}