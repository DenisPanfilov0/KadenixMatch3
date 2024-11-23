using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Progress.Data
{
  public class ProgressData
  {
    [JsonProperty("e")] public EntityData EntityData = new();
    [JsonProperty("at")] public DateTime LastSimulationTickTime;
    public ProgressModel ProgressModel;
  }
  
  [Serializable]
  public class ProgressModel
  {
    public int CurrentLevel;
    public List<Level> Levels = new();
  }
  
  [Serializable]
  public class LevelsData
  {
    public List<Level> Levels;
  }

  [Serializable]
  public class Level
  {
    public List<Tile> boardState;
    public string type;
    public List<string> generatableItems;
    public Dictionary<string, int> goals;
    public int moves;
    public int id;
  }

  [Serializable]
  public class Tile
  {
    public int xPos;
    public int yPos;
    public string tileType;
    public List<TileModifier> tileModifiers;
    public List<TileContentModifier> tileContentModifiers;
    public string tileContentType;
    public int tileContentDurability;
  }

  [Serializable]
  public class TileModifier
  {
    public string tileModifierType;
    public int durability;
  }

  [Serializable]
  public class TileContentModifier
  {
    public string tileContentModifierType;
    public int durability;
  }
}