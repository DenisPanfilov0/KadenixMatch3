using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Progress.Data;
using CustomEditor;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public partial class LevelDesignerWindow : EditorWindow
{
    private void OnSaveLevelButtonClicked()
    {
        Level newLevel = new();

        newLevel.boardState = new();
        newLevel.goals = new();
        newLevel.generatableItems = new();
        
        foreach (var cell in gridData)
        {
            Tile newTile = new();

            newTile.xPos = cell.Key.x;
            newTile.yPos = cell.Key.y;
            
            foreach (var tile in cell.Value)
            {
                if (newTile.tileModifiers == null)
                {
                    newTile.tileModifiers = new();
                }

                if (newTile.tileContentModifiers == null)
                {
                    newTile.tileContentModifiers = new();
                }
                
                if (tile.TileType == TileType.Tile)
                {
                    newTile.tileType = tile.Type.ToString();
                }
                else if (tile.TileType == TileType.TileModifier)
                {
                    newTile.tileModifiers.Add(new TileModifier()
                    {
                        tileModifierType = tile.Type.ToString(),
                        durability = tile.Durability,
                    });
                }
                else if (tile.TileType == TileType.TileContent)
                {
                    newTile.tileContentDurability = tile.Durability;
                    newTile.tileContentType = tile.Type.ToString();
                }
                else if (tile.TileType == TileType.TileContentModifier)
                {
                    newTile.tileContentModifiers.Add(new TileContentModifier()
                    {
                        tileContentModifierType = tile.Type.ToString(),
                        durability = tile.Durability,
                    });
                }
            }
            
            newLevel.boardState.Add(newTile);
        }

        // foreach (var goal in topBarGoals)
        // {
        //     newLevel.goals.Add(goal.Type, topBarGoalsValues);
        // }

        for (int i = 0; i < topBarGoals.Count; i++)
        {
            newLevel.goals.Add(topBarGoals[i].Type.ToString(), int.Parse(topBarGoalsValues[i]));
        }
        
        foreach (var generatorTile in barGenerator)
        {
            newLevel.generatableItems.Add(generatorTile.Type.ToString());
        }

        newLevel.moves = movesCount;
        newLevel.id = int.Parse(levelId);

        SaveLevelToFile(newLevel);
    }
    
    private void SaveLevelToFile(Level newLevel)
    {
        if (levels == null)
        {
            Debug.LogError("Levels файл не выбран! Сохранение невозможно.");
            return;
        }

        // Парсим существующие уровни
        LevelsData levelsData = JsonConvert.DeserializeObject<LevelsData>(levels.text);
        if (levelsData == null || levelsData.Levels == null)
        {
            levelsData = new LevelsData { Levels = new List<Level>() };
        }

        // Проверяем, существует ли уровень с таким ID
        Level existingLevel = levelsData.Levels.FirstOrDefault(level => level.id == newLevel.id);

        if (existingLevel != null)
        {
            // Заменяем существующий уровень
            int index = levelsData.Levels.IndexOf(existingLevel);
            levelsData.Levels[index] = newLevel;
            Debug.Log($"Уровень с ID {newLevel.id} обновлён.");
        }
        else
        {
            // Добавляем новый уровень
            levelsData.Levels.Add(newLevel);
            Debug.Log($"Новый уровень с ID {newLevel.id} добавлен.");
        }

        // Сериализуем обновленные данные обратно в JSON
        string updatedJson = JsonConvert.SerializeObject(levelsData, Formatting.Indented);

        // Сохраняем в файл
        string filePath = AssetDatabase.GetAssetPath(levels);
        File.WriteAllText(filePath, updatedJson);

        // Обновляем AssetDatabase
        AssetDatabase.Refresh();

        Debug.Log("Уровни успешно сохранены.");
    }
}