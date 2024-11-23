using System.Collections.Generic;
using Code.Gameplay.Features.BoardBuildFeature;
using Code.Gameplay.Features.FindMatchesFeature;

namespace Code.Gameplay.Common.Extension
{
    public static class TileTypeParserExtensions
    {
        public static TileTypeId TileTypeResolve(string tileName)
        {
            _tileNameType.TryGetValue(tileName, out TileTypeId tileType);
            return tileType;
        }        
        
        public static TileTypeId TileTypeResolve(FigureTypeId tileName)
        {
            _tileFigureType.TryGetValue(tileName, out TileTypeId tileType);
            return tileType;
        }
        
        public static string AdvancedPowerUpTypeResolve(List<TileTypeId> tilesType)
        {
            string key = string.Join(",", tilesType);
            _tileAdvancedPowerUpType.TryGetValue(key, out string tileType);
            return tileType;
        }

        private static Dictionary<string, TileTypeId> _tileNameType = new Dictionary<string, TileTypeId>()
        {
            {"commonLight", TileTypeId.commonLight },
            {"commonDark", TileTypeId.commonDark },
            
            {"coloredBlue", TileTypeId.coloredBlue },
            {"coloredGreen", TileTypeId.coloredGreen },
            {"coloredPurple", TileTypeId.coloredPurple },
            {"coloredRed", TileTypeId.coloredRed },
            {"coloredYellow", TileTypeId.coloredYellow },
            
            {"tileModifierSpawner", TileTypeId.tileModifierSpawner },
            {"grassModifier", TileTypeId.grassModifier },
            
            {"iceModifier", TileTypeId.iceModifier },
        };
        
        private static Dictionary<FigureTypeId, TileTypeId> _tileFigureType = new Dictionary<FigureTypeId, TileTypeId>()
        {
            {FigureTypeId.LineFourVerticalShapeFigure, TileTypeId.powerUpHorizontalRocket },
            {FigureTypeId.LineFourHorizontalShapeFigure, TileTypeId.powerUpVerticalRocket },
            {FigureTypeId.LineFiveShapeFigure, TileTypeId.powerUpMagicBall },
            {FigureTypeId.CornerShapeFigure, TileTypeId.powerUpBomb },
            {FigureTypeId.TreeShapeFigure, TileTypeId.powerUpBomb },
        };
        
        private static Dictionary<string, string> _tileAdvancedPowerUpType = new Dictionary<string, string>()
        {
            { "powerUpBomb,powerUpBomb", "powerUpBombAndBomb" },
            
            { "powerUpVerticalRocket,powerUpVerticalRocket", "powerUpRocketAndRocket" },
            { "powerUpHorizontalRocket,powerUpHorizontalRocket", "powerUpRocketAndRocket" },
            { "powerUpVerticalRocket,powerUpHorizontalRocket", "powerUpRocketAndRocket" },
            { "powerUpHorizontalRocket,powerUpVerticalRocket", "powerUpRocketAndRocket" },
            
            { "powerUpBomb,powerUpVerticalRocket", "powerUpBombAndRocket" },
            { "powerUpBomb,powerUpHorizontalRocket", "powerUpBombAndRocket" },
            { "powerUpVerticalRocket,powerUpBomb", "powerUpBombAndRocket" },
            { "powerUpHorizontalRocket,powerUpBomb", "powerUpBombAndRocket" },
        };
    }
}