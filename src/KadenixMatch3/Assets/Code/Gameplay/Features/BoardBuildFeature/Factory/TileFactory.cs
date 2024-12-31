using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.BoardBuildFeature.Factory
{
    public class TileFactory : ITileFactory
    {
        private readonly IIdentifierService _identifiers;

        public TileFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }
    
        public GameEntity CreateTile(TileTypeId typeId, Vector3 at, Vector2Int boardPosition, int positionInCoverageQueue = default, int durability = default)
        {
            switch (typeId)
            {
                case TileTypeId.commonLight or TileTypeId.commonDark:
                    return CreateTileBoard(at, typeId, boardPosition);
                
                case TileTypeId.tileModifierSpawner:
                    return CreateTileSpawner(at, typeId, boardPosition);
                
                case TileTypeId.coloredBlue or TileTypeId.coloredGreen or TileTypeId.coloredPurple or TileTypeId.coloredRed or TileTypeId.coloredYellow:
                    return CreateCrystal(at, typeId, boardPosition, positionInCoverageQueue);                
                
                case TileTypeId.powerUpHorizontalRocket:
                    return CreateHorizontalRocket(at, typeId, boardPosition, positionInCoverageQueue);                
                
                case TileTypeId.powerUpVerticalRocket:
                    return CreateVerticalRocket(at, typeId, boardPosition, positionInCoverageQueue);
                
                case TileTypeId.powerUpMagicBall:
                    return CreateMagicBall(at, typeId, boardPosition, positionInCoverageQueue);
                
                case TileTypeId.powerUpBomb:
                    return CreateBomb(at, typeId, boardPosition, positionInCoverageQueue);
                
                case TileTypeId.grassModifier:
                    return CreateGrass(at, typeId, boardPosition, positionInCoverageQueue, durability);
                
                case TileTypeId.iceModifier:
                    return CreateIce(at, typeId, boardPosition, positionInCoverageQueue, durability);
            }

            throw new Exception($"Enemy with type id {typeId} does not exist");
        }

        private GameEntity CreateTileBoard(Vector3 at, TileTypeId typeId, Vector2Int boardPosition)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(0)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .With(x => x.isBoardTile = true)
                ;
        }

        private GameEntity CreateTileSpawner(Vector3 at, TileTypeId typeId, Vector2Int boardPosition)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddTileType(typeId)
                    .With(x => x.isTileSpawner = true)
                ;
        }

        private GameEntity CreateCrystal(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddAccelerationTime(0.2f)
                    .AddFallingSpeed(4f)
                    .AddTileDurability(1)
                    .AddDamageReceived(0)
                    .With(x => x.isTileContent = true)
                    .With(x => x.isColoredCrystal = true)
                    .With(x => x.isSwappable = true)
                    .With(x => x.isMatchable = true)
                    .With(x => x.isMovable = true)
                ;
        }

        private GameEntity CreateHorizontalRocket(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddAccelerationTime(0.2f)
                    .AddFallingSpeed(4f)
                    .AddInteractionStep(1)
                    .AddTileDurability(1)
                    .AddDamageReceived(0)
                    .With(x => x.isTileContent = true)
                    .With(x => x.isTilePowerUp = true)
                    .With(x => x.isSwappable = true)
                    .With(x => x.isMatchable = true)
                    .With(x => x.isPowerUpHorizontalRocket = true)
                    .With(x => x.isPowerUpSpawnAnimation = true)
                    .With(x => x.isMovable = true)
                ;
        }

        private GameEntity CreateVerticalRocket(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddAccelerationTime(0.2f)
                    .AddFallingSpeed(4f)
                    .AddInteractionStep(1)
                    .AddTileDurability(1)
                    .AddDamageReceived(0)
                    .With(x => x.isTileContent = true)
                    .With(x => x.isTilePowerUp = true)
                    .With(x => x.isSwappable = true)
                    .With(x => x.isMatchable = true)
                    .With(x => x.isPowerUpVerticalRocket = true)
                    .With(x => x.isPowerUpSpawnAnimation = true)
                    .With(x => x.isMovable = true)
                ;
        }

        private GameEntity CreateMagicBall(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddAccelerationTime(0.2f)
                    .AddFallingSpeed(4f)
                    .AddInteractionStep(1)
                    .AddTileDurability(1)
                    .AddDamageReceived(0)
                    .With(x => x.isTileContent = true)
                    .With(x => x.isTilePowerUp = true)
                    .With(x => x.isSwappable = true)
                    .With(x => x.isMatchable = true)
                    .With(x => x.isPowerUpMagicalBall = true)
                    .With(x => x.isPowerUpSpawnAnimation = true)
                    .With(x => x.isMovable = true)
                ;
        }

        private GameEntity CreateBomb(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddAccelerationTime(0.2f)
                    .AddFallingSpeed(4f)
                    .AddInteractionStep(1)
                    .AddTileDurability(1)
                    .AddDamageReceived(0)
                    .With(x => x.isTileContent = true)
                    .With(x => x.isTilePowerUp = true)
                    .With(x => x.isSwappable = true)
                    .With(x => x.isMatchable = true)
                    .With(x => x.isPowerUpBomb = true)
                    .With(x => x.isPowerUpSpawnAnimation = true)
                    .With(x => x.isMovable = true)
                ;
        }

        private GameEntity CreateGrass(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue, int durability)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddTileDurability(durability)
                    .AddDamageReceived(0)
                    .With(x => x.isTileModifier = true)
                    .With(x => x.isGrassModifier = true)
                    .With(x => x.isTileFallableSurface = true)
                    .With(x => x.isNotMovable = true)
                    .With(x => x.isInactiveStartState = true)
                ;
        }
        
        private GameEntity CreateIce(Vector3 at, TileTypeId typeId, Vector2Int boardPosition, int positionInCoverageQueue, int durability)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBoardPosition(boardPosition)
                    .AddPositionInCoverageQueue(positionInCoverageQueue)
                    .AddTileType(typeId)
                    .AddViewAddress(typeId.ToString())
                    .AddTileDurability(durability)
                    .AddDamageReceived(0)
                    .With(x => x.isTileContentModifier = true)
                    .With(x => x.isIceModifier = true)
                    .With(x => x.isTransparentToMatch = true)
                    .With(x => x.isNotMovable = true)
                    .With(x => x.isInactiveStartState = true)
                    // .With(x => x.isIndirectInteractable = true) --- если вдруг лёд станет восприимчив к интеракциям рядом уничтоженных кристаллов
                ;
        }
    }
}