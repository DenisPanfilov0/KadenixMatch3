using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.BoardState.Systems
{
    public class BoardStateSetupSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty()
                .With(x => x.isBoardState = true);
        }
    }
}