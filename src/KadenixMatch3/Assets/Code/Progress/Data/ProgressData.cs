using System;
using System.Collections.Generic;
using Code.Meta.Feature.Shop;
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
    public int Coins;
    public int Heart;
    public int MaxHeart;
    public long LastLifeRegenerationTime;
    public List<Level> Levels;
    public SequentialLevelsReward SequentialLevelsReward;
    public CharacterPreBoosters CharacterPreBoosters;
    public CharacterSkills CharacterSkills;
    public List<ShopItemId> PreBoostersSelectedInLevel = new();

    public ProgressModel()
    {
      CurrentLevel = 6;
      Coins = 2000;
      Heart = 5;
      MaxHeart = 5;
      Levels = new();
      SequentialLevelsReward = new SequentialLevelsReward();
      CharacterPreBoosters = new CharacterPreBoosters();
      CharacterSkills = new CharacterSkills();
    }
  }

  [Serializable]
  public class CharacterPreBoosters
  {
    public int PreBoosterBomb;
    public int PreBoosterLinearLightning;
    public int PreBoosterMagicBall;

    public CharacterPreBoosters()
    {
      PreBoosterBomb = 1;
      PreBoosterLinearLightning = 2;
      PreBoosterMagicBall = 4;
    }
  }

  [Serializable]
  public class CharacterSkills
  {
    public int HandSkill;
    public int SwapSkill;
    public int DamageAllBoardSkill;
    
    public CharacterSkills()
    {
      HandSkill = 2;
      SwapSkill = 3;
      DamageAllBoardSkill = 3;
    }
  }
    
}