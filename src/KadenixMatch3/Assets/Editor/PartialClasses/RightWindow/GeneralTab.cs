using CustomEditor;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Code.Progress.Data;
using Newtonsoft.Json;

public partial class LevelDesignerWindow : EditorWindow
{
    private List<string> cellValues = new List<string>(); // Список для хранения значений ячеек
    private int numberOfCells = 5; // Количество ячеек
    private int currentGeneratorTypeCount = 1;

    private string levelId = ""; // Переменная для хранения ID уровня
    private TextAsset levels; // Ссылка на текстовый файл с уровнями
    private LevelsData levelsData; // Объект для хранения данных из levels

    private bool showLevelIds = false; // Переменная для управления состоянием сворачивающегося списка

    private void DrawGeneralTab()
    {
        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);

        config = (LevelDesignerConfig)EditorGUILayout.ObjectField("Level Config", config, typeof(LevelDesignerConfig), false);

        if (config == null)
        {
            EditorGUILayout.HelpBox("LevelDesignerConfig не найден! Проверьте путь или добавьте файл.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.LabelField($"Tiles Count: {config.Tiles?.Count ?? 0}");
        }

        GUILayout.Space(10);

        // Поле для Levels
        levels = (TextAsset)EditorGUILayout.ObjectField("Levels File", levels, typeof(TextAsset), false);
        if (levels == null)
        {
            EditorGUILayout.HelpBox("Levels файл не выбран! Добавьте файл в формате .txt.", MessageType.Warning);
        }
        else
        {
            if (GUILayout.Button("Parse Levels"))
            {
                ParseLevelsFile();
            }

            if (levelsData != null)
            {
                EditorGUILayout.LabelField($"Parsed Levels Count: {levelsData.Levels?.Count ?? 0}");

                // Сворачивающийся список ID уровней
                showLevelIds = EditorGUILayout.Foldout(showLevelIds, "Show Level IDs");
                if (showLevelIds && levelsData.Levels != null)
                {
                    EditorGUI.indentLevel++;
                    foreach (var level in levelsData.Levels)
                    {
                        EditorGUILayout.LabelField($"Level ID: {level.id}");
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }

        GUILayout.Space(10);

        // Поле для ввода ID уровня
        EditorGUILayout.LabelField("Level Identification", EditorStyles.boldLabel);
        levelId = EditorGUILayout.TextField("Level ID:", levelId);

        GUILayout.Space(10);

        // Блок с генераторами
        Rect labelRect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.boldLabel, GUILayout.Height(40));
        EditorGUI.DrawRect(labelRect, new Color(0.8f, 0.8f, 1f));
        EditorGUI.LabelField(new Rect(labelRect.x + 10, labelRect.y + 5, labelRect.width, 30), "GeneratableTile",
            new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
                normal = { textColor = Color.black }
            });

        Rect blockRect = EditorGUILayout.BeginVertical("box");
        EditorGUI.DrawRect(blockRect, new Color(0.95f, 0.95f, 0.95f));

        // Рисуем ячейки
        DrawCells(blockRect);

        EditorGUILayout.EndVertical();
    }


    private void DrawCells(Rect blockRect)
    {
        float startX = blockRect.x + 10;
        float startY = blockRect.y + 10;

        float topBarWidth = 5 * cellSize;
        float topBarHeight = cellSize;

        float offsetX = (gridSize * cellSize - topBarWidth) / 2f;

        // Рисуем ячейки на основе currentGoalCount
        for (int i = 0; i < currentGeneratorTypeCount; i++)
        {
            // Смещаем ячейку с учётом inputFieldVerticalOffset
            Rect topBarCellRect = new Rect(startX + offsetX + i * (cellSize + cellSpacing), startY, cellSize, topBarHeight);

            // Рисуем ячейку
            if (openedGeneratorbarIndices.Contains(i))
            {
                EditorGUI.DrawRect(topBarCellRect, new Color(0.8f, 0.8f, 0.95f, 1f));
            }
            else
            {
                EditorGUI.DrawRect(topBarCellRect, new Color(0.9f, 0.9f, 0.9f, 1f));
            }

            Handles.DrawSolidRectangleWithOutline(topBarCellRect, Color.clear, Color.black);

            if (i < barGenerator.Count && barGenerator[i] != null)
            {
                GUI.DrawTexture(topBarCellRect, barGenerator[i].Sprite.texture);
            }

            // Кнопка "+"
            if (GUI.Button(topBarCellRect, "+", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 18,
                normal = { textColor = Color.white },
                fontStyle = FontStyle.Bold
            }))
            {
                HandleGoalsTopBarClickGenerator(i);
            }

            // Крестик в правом верхнем углу ячейки
            Rect closeButtonRect = new Rect(topBarCellRect.x + topBarCellRect.width + cellSpacing - 18, topBarCellRect.y, 20, 20);

            GUIStyle closeButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 32,  // Увеличиваем размер шрифта для крестика
                fontStyle = FontStyle.Bold,  // Жирный шрифт
                normal = { background = MakeTextureGenerator(1, 1, Color.white), textColor = Color.red },  // Белый фон и красный текст
                alignment = TextAnchor.MiddleCenter,  // Выравнивание по центру
                stretchWidth = true,  // Чтобы кнопка занимала всю ширину
                stretchHeight = true  // Чтобы кнопка занимала всю высоту
            };

            // Рисуем кнопку крестика
            if (GUI.Button(closeButtonRect, "×", closeButtonStyle))
            {
                HandleRemoveGoalGenerator(i);  // Обработчик удаления цели
            }
        }
    }

    private void HandleRemoveGoalGenerator(int index)
    {
        if (index < 0 || index >= currentGeneratorTypeCount || barGenerator.Count <= 1)
            return;

        // Удаляем цель (объект) из списка
        if (index < barGenerator.Count)
            barGenerator.RemoveAt(index);

        // Уменьшаем количество отображаемых ячеек
        currentGeneratorTypeCount--;

        while (cellValues.Count < currentGeneratorTypeCount)
        {
            cellValues.Add("0"); // Добавляем значение по умолчанию
        }

        // Обновляем UI
        Repaint();
    }

    private void HandleGoalsTopBarClickGenerator(int index)
    {
        selectedGeneratorBarIndex = index;
        openedGeneratorIndices.Add(index);
        Repaint();
        OpenWindowGenerator(new Vector2Int(index, 0));
    }

    private Texture2D MakeTextureGenerator(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    private void OpenWindowGenerator(Vector2Int cellPosition)
    {
        selectedCell = cellPosition;
        windowRect.x = selectedCell.x * cellSize + 10;
        windowRect.y = selectedCell.y * cellSize + 10;

        isWindowOpenGenerator = true;
    }

    private void DrawWindowGenerator()
    {
        if (isWindowOpenGenerator)
        {
            EditorGUI.DrawRect(windowRect, new Color(0f, 0f, 0f, 0.8f));
            Handles.DrawSolidRectangleWithOutline(windowRect, new Color(0f, 0f, 0f, 0.8f), Color.white);

            Rect closeButtonRect = new Rect(windowRect.x + windowRect.width - 30, windowRect.y + 10, 20, 20);
            if (GUI.Button(closeButtonRect, "X", new GUIStyle() { fontSize = 16, normal = { textColor = Color.white } }))
            {
                CloseWindowGenerator();
            }

            DrawTileSelectionGenerator();
        }
    }

    private void DrawTileSelectionGenerator()
    {
        float tileSize = 50f;
        float startX = windowRect.x + 10;
        float startY = windowRect.y + 40;

        TileItem[] tiles = { coloredRedTile, coloredPurpleTile, coloredBlueTile, coloredYellowTile, coloredGreenTile };
        string[] tileNames = { "Red", "Purple", "Blue", "Yellow", "Green" };

        float currentX = startX;
        float currentY = startY;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (currentX + tileSize > windowRect.x + windowRect.width - 10)
            {
                currentX = startX;
                currentY += tileSize + 10;
            }

            GUI.DrawTexture(new Rect(currentX, currentY, tileSize, tileSize), tiles[i].Sprite.texture);

            if (Event.current.type == EventType.MouseDown && 
                new Rect(currentX, currentY, tileSize, tileSize).Contains(Event.current.mousePosition))
            {
                AddTileToTopBarGenerator(tiles[i]);
                CloseWindowGenerator();
            }

            currentX += tileSize + 10;
        }
    }

    private void AddTileToTopBarGenerator(TileItem tile)
    {
        // Добавляем новый объект в список
        currentGeneratorTypeCount++;

        if (barGenerator.Count - 1 > selectedGeneratorBarIndex || barGenerator.Count >= 4)
        {
            currentGeneratorTypeCount--;
        }

        // Добавляем новый объект в список
        if (selectedGeneratorBarIndex < 0 || selectedGeneratorBarIndex >= 5)
        {
            return;
        }

        while (barGenerator.Count <= selectedGeneratorBarIndex)
        {
            barGenerator.Add(null);
        }

        barGenerator[selectedGeneratorBarIndex] = tile;
        Repaint();
        CloseWindowGenerator();
    }

    private void CloseWindowGenerator()
    {
        isWindowOpenGenerator = false;
        Repaint();
    }
    
    private void ParseLevelsFile()
    {
        try
        {
            levelsData = JsonConvert.DeserializeObject<LevelsData>(levels.text);
            Debug.Log($"Levels parsed successfully. Total levels: {levelsData.Levels.Count}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to parse levels: {ex.Message}");
        }
    }
}
