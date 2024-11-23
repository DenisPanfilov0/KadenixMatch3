using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Extension;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.BindingTilesFeature.Systems
{
    public sealed class BindingTilesSystem : IExecuteSystem
    {
        // private readonly GameEntity _context;

        public BindingTilesSystem(Contexts contexts)
        {
            // _context = contexts.tiles;
        }

        public void Execute()
        {
            // var board = _context.boardState.BoardSize;

            for (var x = 0; x < 13; x++)
            {
                for (var y = 0; y <= 13; y++)
                {
                    var position = new Vector2Int(x, y);
                    var tileEntities = TileUtilsExtensions.GetTilesInCell(position);
                    if (tileEntities != null && tileEntities.Count > 0 && !tileEntities.Any(x => !x.hasPositionInCoverageQueue))
                    {
                        TilesRebinding(tileEntities);
                    }
                }
            }
        }

        private void TilesRebinding(HashSet<GameEntity> tileEntities)
        {
            // foreach (var entity in tileEntities)
            // {
            //     if (entity.GenerateTile || entity.isStoryDestinationTileModifier)
            //     {
            //         return;
            //     }
            // }

            // if (tileEntities.Any(x => !x.hasPositionInCoverageQueue))
            // {
            //     return;
            // }
            
            var list = tileEntities.OrderByDescending(e => e.PositionInCoverageQueue).ToList();
            var orderQueue = 0;

            for (int i = list.Count; i > 0; i--)
            {
                list[^i].ReplacePositionInCoverageQueue(i - 1);
                list[^i].ReplaceTargetId(orderQueue);
                orderQueue = list[^i].Id;
            }
        }
    }
}