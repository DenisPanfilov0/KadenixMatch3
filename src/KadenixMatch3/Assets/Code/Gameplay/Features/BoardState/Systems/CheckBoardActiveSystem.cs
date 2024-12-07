using Entitas;

namespace Code.Gameplay.Features.BoardState.Systems
{
    public class CheckBoardActiveSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tiles;
        private readonly IGroup<GameEntity> _boards;

        public CheckBoardActiveSystem(GameContext game)
        {
            _tiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.TileType));
            
            _boards = game.GetGroup(GameMatcher.AllOf(GameMatcher.BoardState));
        }

        public void Execute()
        {
            bool isActive = false;
            
            foreach (GameEntity tile in _tiles)
            {
                if (!CheckTile(tile))
                {
                    isActive = true;
                }
            }

            foreach (GameEntity board in _boards)
            {
                board.isBoardActiveInteraction = isActive;
            }
        }

        private bool CheckTile(GameEntity tile)
        {
            if (tile.isAnimationProcess || tile.isProcessedFalling || tile.isDestructed || tile.isFindMatchesProcess || tile.isTileSwipeProcessed)
            {
                return false;
            }

            return true;
        }
    }
}