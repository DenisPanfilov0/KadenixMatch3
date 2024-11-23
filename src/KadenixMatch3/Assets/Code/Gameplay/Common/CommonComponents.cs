using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.Tiles.Behaviours;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Code.Gameplay.Common
{
  [Game, Meta] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
  [Game] public class EntityLink : IComponent { [EntityIndex] public int Value; }
  
  [Game] public class WorldPosition : IComponent { public Vector3 Value; }
  
  [Game] public class Active : IComponent { }
 
  [Game] public class TransformComponent : IComponent { public Transform Value; }
  [Game] public class SpriteRendererComponent : IComponent { public SpriteRenderer Value; }
  
  
  [Game] public class AnimationProcess : IComponent { }
  [Game] public class TileActiveProcess : IComponent { }
  [Game] public class DestructedProcess : IComponent { }
  
  [Game] public class BoardState : IComponent { }
  
  [Game] public class BoardTile : IComponent { }
  [Game] public class CanSwap : IComponent { }
  
  [Game] public class TileContent : IComponent { }
  [Game] public class TileModifier : IComponent { }
  [Game] public class TileContentModifier : IComponent { }
  [Game] public class TilePowerUp : IComponent { }
  [Game] public class TileSpawner : IComponent { }
  [Game] public class TileType : IComponent { public TileTypeId Value; }
  
  [Game] public class TileDurability : IComponent { public int Value; }
  [Game] public class DamageReceived : IComponent { public int Value; }
  
  [Game] public class BoardPosition : IComponent { [EntityIndex] public Vector2Int Value; }
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class PositionInCoverageQueue : IComponent { public int Value; }
  [Game] public class Swappable : IComponent { }
  [Game] public class Matchable : IComponent { }
  [Game] public class Movable : IComponent { }
  [Game] public class NotMovable : IComponent { }
  [Game] public class TileFallableSurface : IComponent { } //тайлы как трава, на них можно падать
  [Game] public class TransparentToMatch : IComponent { } //тайлы как лёд, прозрачный для матчинга
  
  [Game] public class DirectInteraction : IComponent { }
  [Game] public class IndirectInteraction : IComponent { }
  [Game] public class IndirectInteractable : IComponent { }
  
  
  [Game] public class TileTweenAnimationComponent : IComponent { public TileTweenAnimation Value; }
  [Game] public class PowerUpTileTweenAnimationComponent : IComponent { public PowerUpTileTweenAnimation Value; }
  [Game] public class GrassModifierAnimationComponent : IComponent { public GrassModifierAnimation Value; }
  [Game] public class IceModifierAnimationComponent : IComponent { public IceModifierAnimation Value; }
}