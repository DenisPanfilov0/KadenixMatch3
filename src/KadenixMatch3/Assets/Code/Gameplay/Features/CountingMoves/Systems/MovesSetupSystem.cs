using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.Features.CountingMoves.Services;
using Code.Progress.Data;
using Code.Progress.Provider;
using Entitas;

namespace Code.Gameplay.Features.CountingMoves.Systems
{
    public sealed class MovesSetupSystem : IInitializeSystem
    {
        private readonly IProgressProvider _progress;
        private readonly IMovesInGameService _movesInGameService;

        public MovesSetupSystem(IProgressProvider progress, IMovesInGameService movesInGameService)
        {
            _progress = progress;
            _movesInGameService = movesInGameService;
        }
        
        public void Initialize()
        {
            Level lvl = _progress.ProgressData.ProgressModel.Levels.FirstOrDefault(x =>
                x.id == _progress.ProgressData.ProgressModel.CurrentLevel);

            CreateEntity.Empty()
                .AddMoves(lvl.moves);
            
            _movesInGameService.SetMoves(lvl.moves);
        }
    }
}