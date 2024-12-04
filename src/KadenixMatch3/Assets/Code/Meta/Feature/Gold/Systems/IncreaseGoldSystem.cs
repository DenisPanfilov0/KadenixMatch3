using System.Collections.Generic;
using Code.Meta.Feature.Gold.Services;
using Entitas;

namespace Code.Meta.Feature.Gold.Systems
{
    public class IncreaseGoldSystem : IExecuteSystem
    {
        private readonly ICharacterGoldUIService _characterGoldUIService;
        private readonly IGroup<MetaEntity> _goldIncreases;
        private List<MetaEntity> _buffer = new(4);

        public IncreaseGoldSystem(MetaContext contextParameter, ICharacterGoldUIService characterGoldUIService)
        {
            _characterGoldUIService = characterGoldUIService;
            _goldIncreases = contextParameter.GetGroup(MetaMatcher
                .AllOf(MetaMatcher.IncreaseGold)
                .NoneOf(MetaMatcher.Destructed));
        }

        public void Execute()
        {
            foreach (MetaEntity goldIncrease in _goldIncreases.GetEntities(_buffer))
            {
                _characterGoldUIService.IncreaseGold(goldIncrease.IncreaseGold);
                goldIncrease.isDestructed = true;
            }
        }
    }
}