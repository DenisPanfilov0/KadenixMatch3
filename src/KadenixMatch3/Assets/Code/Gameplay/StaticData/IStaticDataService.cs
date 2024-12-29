using System.Collections.Generic;
using Code.Config;
using Code.Gameplay.Features.GoalsCounting.Configs;
using Code.Gameplay.Windows;
using Code.Meta.Feature.Shop;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    
    GameObject GetWindowPrefab(WindowId id);
    List<ShopItemConfig> GetShopItemsConfig();
    CostToContinuePlayingConfig GetCostToContinuePlayingConfig();
    DebugGameSettings GetDebugGameSettings();
  }
}