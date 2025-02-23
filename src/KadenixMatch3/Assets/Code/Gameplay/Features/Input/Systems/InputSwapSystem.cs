using System.Collections.Generic;
using Code.Config;
using Code.Gameplay.Features.FindMatchesFeature;
using Code.Gameplay.Features.FindMatchesFeature.Services;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
    public class InputSwapSystem : IExecuteSystem
    {
        private readonly IFindMatchesService _findMatchesService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _firstSelectTiles;
        private readonly IGroup<GameEntity> _secondSelectTiles;
        private readonly IGroup<GameEntity> _moves;
        private List<GameEntity> _buffer = new(4);
        private List<GameEntity> _bufferSecodn = new(4);
        private List<GameEntity> _bufferMoves = new(4);
        private readonly DebugGameSettings _debugGameSettings;

        public InputSwapSystem(GameContext game, IFindMatchesService findMatchesService,
            IStaticDataService staticDataService)
        {
            _findMatchesService = findMatchesService;
            _staticDataService = staticDataService;
            _firstSelectTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.FirstSelectTileSwipe)
                .NoneOf(GameMatcher.TileSwipeProcessed));
            _secondSelectTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.SecondSelectTileSwipe)
                .NoneOf(GameMatcher.TileSwipeProcessed));
            _moves = game.GetGroup(GameMatcher.AllOf(GameMatcher.Moves).NoneOf(GameMatcher.MovesChangeAmountProcess));

            _debugGameSettings = _staticDataService.GetDebugGameSettings();
        }

        public void Execute()
        {
            foreach (var firstSelectTile in _firstSelectTiles.GetEntities(_buffer))
            foreach (var secondSelectTile in _secondSelectTiles.GetEntities(_bufferSecodn))
            {
                var firstTilePosition = firstSelectTile.BoardPosition;
                var secondTilePosition = secondSelectTile.BoardPosition;

                firstSelectTile.ReplaceBoardPosition(secondTilePosition);
                secondSelectTile.ReplaceBoardPosition(firstTilePosition);

                firstSelectTile.isTileSwipeProcessed = true;
                secondSelectTile.isTileSwipeProcessed = true;

                secondSelectTile.TileTweenAnimation.SetStartTransform();
                firstSelectTile.TileTweenAnimation.SetStartTransform();

                int firstTileQueue = firstSelectTile.PositionInCoverageQueue;
                int secondTileQueue = secondSelectTile.PositionInCoverageQueue;

                secondSelectTile.ReplacePositionInCoverageQueue(firstTileQueue);
                firstSelectTile.ReplacePositionInCoverageQueue(secondTileQueue);

                firstSelectTile.ReplaceSwipeDirection(
                    new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y)
                    - new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y));

                secondSelectTile.ReplaceSwipeDirection(
                    new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y)
                    - new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y));

                if (!_debugGameSettings.FreeSwapWithoutMatches)
                {
                    List<GameEntity> firstIdenticalTiles = _findMatchesService.FindIdenticalTiles(firstSelectTile);
                    bool firstMatches = false;

                    if (firstIdenticalTiles.Count >= 3)
                    {
                        firstSelectTile.isTileForCheckedMatch = true;
                        firstMatches = _findMatchesService.CheckMatches(firstIdenticalTiles);
                        firstSelectTile.isTileForCheckedMatch = false;
                    }

                    List<GameEntity> secondIdenticalTiles = _findMatchesService.FindIdenticalTiles(secondSelectTile);
                    bool secondMatches = false;

                    if (secondIdenticalTiles.Count >= 3)
                    {
                        secondSelectTile.isTileForCheckedMatch = true;
                        secondMatches = _findMatchesService.CheckMatches(secondIdenticalTiles);
                        secondSelectTile.isTileForCheckedMatch = false;
                    }

                    if (firstMatches || secondMatches || firstSelectTile.isTilePowerUp ||
                        secondSelectTile.isTilePowerUp)
                    {
                        Debug.Log("Матчи были перед ходом");

                        foreach (GameEntity move in _moves.GetEntities(_bufferMoves))
                        {
                            move.AddDecreaseMoves(1);
                            move.isMovesChangeAmountProcess = true;
                        }

                        return;
                    }
                    else
                    {
                        Debug.Log("Ходов не было");

                        firstSelectTile.isTileSwipeProcessed = false;
                        secondSelectTile.isTileSwipeProcessed = false;

                        firstSelectTile.ReplaceBoardPosition(firstTilePosition);
                        secondSelectTile.ReplaceBoardPosition(secondTilePosition);

                        secondSelectTile.ReplacePositionInCoverageQueue(firstTileQueue);
                        firstSelectTile.ReplacePositionInCoverageQueue(secondTileQueue);

                        firstSelectTile.isFirstSelectTileSwipe = false;
                        secondSelectTile.isSecondSelectTileSwipe = false;

                        // firstSelectTile.ReplaceSwipeDirection(
                        //     new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y)
                        //     - new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y));
                        //
                        // secondSelectTile.ReplaceSwipeDirection(
                        //     new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y)
                        //     - new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y));
                        
                        // firstSelectTile.isTileSwipeProcessed = false;
                        // secondSelectTile.isTileSwipeProcessed = false;

                        return;
                    }
                }
                else
                {
                    foreach (GameEntity move in _moves.GetEntities(_bufferMoves))
                    {
                        move.AddDecreaseMoves(1);
                        move.isMovesChangeAmountProcess = true;
                    }
                }



                // firstSelectTile.Transform.position = new Vector3(firstSelectTile.BoardPosition.x, firstSelectTile.BoardPosition.y);
                // secondSelectTile.Transform.position = new Vector3(secondSelectTile.BoardPosition.x, secondSelectTile.BoardPosition.y);
            }
        }
    }
}