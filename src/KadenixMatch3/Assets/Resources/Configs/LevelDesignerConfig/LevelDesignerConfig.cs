using System;
using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature;
using UnityEngine;

namespace CustomEditor
{
    [CreateAssetMenu(menuName = "ECS Survivors/LevelDesignerConfig", fileName = "LevelDesignerConfig")]
    public class LevelDesignerConfig : ScriptableObject
    {
        public List<TileItem> Tiles;
    }

    [Serializable]
    public class TileItem
    {
        public Sprite Sprite;
        public string Name => Sprite.name;
        public TileTypeId Type;
        public int Durability;
        public TileType TileType;
        public int Layer;
        
        [NonSerialized] public Rect GuiRect;
    }

    public enum TileType
    {
        Tile = 0,
        TileModifier = 1,
        TileContent = 2,
        TileContentModifier = 3
    }
    
    public enum GeneratableItems
    {
        Unknown = 0,
        colorGreen = 1,
        colorRed = 2,
        colorBlue = 3,
        colorYellow = 4,
        colorPurple = 5,
    }
}