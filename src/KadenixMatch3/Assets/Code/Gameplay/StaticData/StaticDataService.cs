using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using Code.Meta.Feature.Shop;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, GameObject> _windowPrefabsById;
    private List<ShopItemConfig> _shopItemConfigs;

    public void LoadAll()
    {
      LoadWindows();
      LoadShopItems();
    }

    public GameObject GetWindowPrefab(WindowId id) =>
      _windowPrefabsById.TryGetValue(id, out GameObject prefab)
        ? prefab
        : throw new Exception($"Prefab config for window {id} was not found");

    public List<ShopItemConfig> GetShopItemsConfig() =>
      _shopItemConfigs;

    private void LoadWindows()
    {
      _windowPrefabsById = Resources
        .Load<WindowsConfig>("Configs/Windows/windowsConfig")
        .WindowConfigs
        .ToDictionary(x => x.Id, x => x.Prefab);
    }
    
    private void LoadShopItems()
    {
      _shopItemConfigs = Resources
        .Load<ShopItemsConfig>("Configs/ShopItems/shopItemConfig")
        .ShopItemConfigs
        .ToList();
    }
  }
}