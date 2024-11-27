using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Serialization;

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
    public List<Level> Levels;
    public SequentialLevelsReward SequentialLevelsReward;
    public CharacterBoosters CharacterBoosters;

    public ProgressModel()
    {
      CurrentLevel = 1;
      Levels = new();
      SequentialLevelsReward = new SequentialLevelsReward();
      CharacterBoosters = new CharacterBoosters();
    }
  }

  [Serializable]
  public class CharacterBoosters
  {
    public int HandSkill;
    public int SwapSkill;

    public CharacterBoosters()
    {
      HandSkill = 5;
      SwapSkill = 0;
    }
  }
}