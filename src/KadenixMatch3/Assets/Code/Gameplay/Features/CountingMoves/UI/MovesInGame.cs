using Code.Gameplay.Features.CountingMoves.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.CountingMoves.UI
{
    public class MovesInGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _count;
        private IMovesInGameService _movesInGameService;

        [Inject]
        public void Construct(IMovesInGameService movesInGameService)
        {
            _movesInGameService = movesInGameService;

            _movesInGameService.MovesChange += MovesChange;
        }

        private void OnDestroy()
        {
            _movesInGameService.MovesChange -= MovesChange;
        }

        private void MovesChange()
        {
            _count.text = _movesInGameService.GetMoves().ToString();
        }
    }
}