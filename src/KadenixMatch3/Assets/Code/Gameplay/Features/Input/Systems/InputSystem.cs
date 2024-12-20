using System.Collections.Generic;
using System.Linq;
using Code.Common.Entity;
using Code.Gameplay.Common.Extension;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
    public class InputSystem : IExecuteSystem
    {
        readonly Contexts _contexts;
        private Vector2Int? _initialTilePosition = null;
        private float _lastClickTime = 0f;
        private readonly IGroup<GameEntity> _skills;
        private const float DoubleClickThreshold = 0.5f;

        public InputSystem(Contexts contexts)
        {
            _contexts = contexts;
            
            _skills = Contexts.sharedInstance.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.CharacterSkillProcessed));
        }

        public void Execute()
        {
            // if (_contexts.game.boardState.IsActive/* && _contexts.tiles.boardState.CanSwap*/)
            // {
            HandleMouseInput();
            // }
        }

        private void HandleMouseInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                var position = new Vector2Int(
                    Mathf.RoundToInt(mouseWorldPos.x),
                    Mathf.RoundToInt(mouseWorldPos.y)
                );

                var singleClickEntity = _contexts.input.GetGroup(InputMatcher.SingleClick).GetSingleEntity();
                
                if (singleClickEntity != null)
                {
                    singleClickEntity.ReplaceSingleClick(position);
                }
                else
                {
                    singleClickEntity = _contexts.input.CreateEntity();
                    singleClickEntity.AddSingleClick(position);
                }

                if (_initialTilePosition.HasValue && _initialTilePosition.Value == position && (Time.realtimeSinceStartup - _lastClickTime) < DoubleClickThreshold)
                {
                    singleClickEntity.ReplaceDoubleClick(position);

                    GameEntity selectedTile = TileUtilsExtensions.GetTopTileByPosition(_initialTilePosition.Value);

                    if (selectedTile.isTilePowerUp)
                    {
                        selectedTile.isTileForDoubleClick = true;
                    }
                    
                    _initialTilePosition = null;
                }
                else
                {
                    _initialTilePosition = position;
                    _lastClickTime = Time.realtimeSinceStartup;
                }
            }
            else if (UnityEngine.Input.GetMouseButton(0) && _initialTilePosition.HasValue)
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                var currentPosition = new Vector2Int(
                    Mathf.RoundToInt(mouseWorldPos.x),
                    Mathf.RoundToInt(mouseWorldPos.y)
                );

                if (currentPosition != _initialTilePosition.Value && AreTilesAdjacent(_initialTilePosition.Value, currentPosition))
                {
                    var singleClickEntity = _contexts.input.GetGroup(InputMatcher.SingleClick).GetSingleEntity();

                    if (singleClickEntity != null)
                    {
                        var firstTile = TileUtilsExtensions.GetTopTileByPosition(_initialTilePosition.Value);
                        var secondTile = TileUtilsExtensions.GetTopTileByPosition(currentPosition);

                        if (firstTile != null && secondTile != null && firstTile.isSwappable && secondTile.isSwappable
                            && !firstTile.isProcessedFalling && !secondTile.isProcessedFalling
                            && !firstTile.isTileSwipeProcessed && !secondTile.isTileSwipeProcessed
                            && !TileUtilsExtensions.GetTilesInCell(firstTile.BoardPosition).Any(x => x.isTileSpawner)
                            && !TileUtilsExtensions.GetTilesInCell(secondTile.BoardPosition).Any(x => x.isTileSpawner)
                            && !firstTile.isDestructedProcess && !secondTile.isDestructedProcess
                            && !firstTile.isActiveInteraction && !secondTile.isActiveInteraction
                            && !firstTile.isAnimationProcess && !secondTile.isAnimationProcess)
                        {
                            if (_skills.count == 0)
                            {
                                if ((firstTile.isTilePowerUp && secondTile.isTilePowerUp) || firstTile.isPowerUpMagicalBall || secondTile.isPowerUpMagicalBall)
                                {
                                    firstTile.isFirstSelectPowerUpSwipe = true;
                                    secondTile.isSecondSelectPowerUpSwipe = true;
                                }
                                else
                                {
                                    firstTile.isFirstSelectTileSwipe = true;
                                    secondTile.isSecondSelectTileSwipe = true;
                                }
                            }
                            else
                            {
                                foreach (var skill in _skills)
                                {
                                    if (skill.isSwapSkillRequest)
                                    {
                                        CreateEntity.Empty()
                                            .AddTilesSkillSwap(new List<Vector2Int>{firstTile.BoardPosition, secondTile.BoardPosition});
                                    }
                                }
                            }
                        }
                        
                        singleClickEntity.ReplaceSingleClick(_initialTilePosition.Value);
                    }

                    _initialTilePosition = null;
                }
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if (_initialTilePosition.HasValue && (Time.realtimeSinceStartup - _lastClickTime) >= DoubleClickThreshold)
                {
                    _initialTilePosition = null;
                }

                var singleClickEntity = _contexts.input.GetGroup(InputMatcher.SingleClick).GetSingleEntity();
                if (singleClickEntity != null && _skills.count == 0)
                {
                    singleClickEntity.Destroy();
                }
            }
        }

        private bool AreTilesAdjacent(Vector2Int pos1, Vector2Int pos2)
        {
            return Mathf.Abs(pos1.x - pos2.x) == 1 && pos1.y == pos2.y ||
                   Mathf.Abs(pos1.y - pos2.y) == 1 && pos1.x == pos2.x;
        }
    }
}